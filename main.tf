terraform {
  required_providers {
    aws = {
      source = "hashicorp/aws"
      version = "4.58.0"
    }
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "=3.0.0"
    }
  }
}

provider "azurerm" {
  features {}
}

variable "db_username" {
  type = string
}
variable "db_password" {
  type = string
}

variable "use_swagger" {
  type = string
}

variable "encryption_key" {
  type = string
}

provider "aws" {
  shared_credentials_files = [".aws/creds/credentials"]
  profile = "default"
}

resource "aws_launch_template" "web" {
  name_prefix = "disclone-WS"
  instance_type = "t2.micro"
  image_id = "ami-005f9685cb30f234b"
}

resource "aws_autoscaling_group" "prod-ASG-WS-disclone" {
  availability_zones = ["us-east-1a"]
  desired_capacity   = 1
  max_size           = 1
  min_size           = 1
  launch_template {
    id      = "${aws_launch_template.web.id}"
    version = "$Latest"
  }
}


resource "azurerm_app_service_plan" "main" {
  name                = "prod-disclone-asp"
  location            = "eastus2"
  resource_group_name = "prod-disclone-group"
  reserved = true
  kind = "Linux"
  sku {
    tier = "Free"
    size = "F1"
  }
}

resource "aws_db_instance" "disclone-prod-db" {
  availability_zone = "us-east-1a"
  instance_class = "db.t3.micro"
  allocated_storage = 16
  db_name = "postgres"
  engine = "postgres"
  username = "${ var.db_username }"
  password = "${ var.db_password }"
  engine_version = "14"
  publicly_accessible = true
  storage_encrypted = false
}

resource "azurerm_linux_web_app" "prod-disclone-app" {
  resource_group_name = "prod-disclone-group"
  name                = "prod-disclone-app"
  location            = "East US 2"
  service_plan_id = azurerm_app_service_plan.main.id
  site_config {
    use_32_bit_worker = true
    always_on = false
    application_stack {
      dotnet_version = "6.0"
    }
  }
  app_settings = {
    "DB_CONNECTION_STRING" : "User ID=postgres;Host=${aws_db_instance.disclone-prod-db.address};Port=5432;Database=postgres;"
    "DB_PASSWORD" : "${ var.db_username }"
    "ENCRYPTION_KEY" : "${ var.encryption_key }"
    "USE_SWAGGER" : "${ var.use_swagger }"
  }
}

resource "azurerm_app_service_source_control" "repo" {
  app_id             = azurerm_linux_web_app.prod-disclone-app.id
  repo_url           = "https://github.com/ninjoma/disclone-api"
  branch             = "develop"
  use_manual_integration = false
  use_mercurial      = false
}
