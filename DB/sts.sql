-- --------------------------------------------------------
-- Хост:                         127.0.0.1
-- Версия сервера:               5.5.38-log - MySQL Community Server (GPL)
-- ОС Сервера:                   Win32
-- HeidiSQL Версия:              8.3.0.4694
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- Дамп структуры базы данных sts
CREATE DATABASE IF NOT EXISTS `sts` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `sts`;


-- Дамп структуры для таблица sts.Asset
CREATE TABLE IF NOT EXISTS `Asset` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Value` decimal(10,2) DEFAULT '0.00',
  `Asset_date` date NOT NULL,
  `Enterprise_id` bigint(20) DEFAULT '0',
  PRIMARY KEY (`Id`),
  KEY `FK_Asset_Enterprise` (`Enterprise_id`),
  CONSTRAINT `FK_Asset_Enterprise` FOREIGN KEY (`Enterprise_id`) REFERENCES `Enterprise` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- Дамп данных таблицы sts.Asset: ~1 rows (приблизительно)
/*!40000 ALTER TABLE `Asset` DISABLE KEYS */;
REPLACE INTO `Asset` (`Id`, `Value`, `Asset_date`, `Enterprise_id`) VALUES
	(1, 0.00, '2015-05-03', 1);
/*!40000 ALTER TABLE `Asset` ENABLE KEYS */;


-- Дамп структуры для таблица sts.Character
CREATE TABLE IF NOT EXISTS `Character` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Title` varchar(50) NOT NULL,
  `Gender` varchar(50) NOT NULL,
  `Level` int(11) NOT NULL DEFAULT '1',
  `GameTime` datetime NOT NULL DEFAULT '2015-01-01 00:00:00',
  `GameDay` varchar(20) NOT NULL DEFAULT '0',
  `GameScene` varchar(20) NOT NULL DEFAULT 'MainMenu',
  `LocationX` double NOT NULL DEFAULT '970',
  `LocationY` double NOT NULL DEFAULT '54.01',
  `LocationZ` double NOT NULL DEFAULT '1383.77',
  `Stage_Id` bigint(20) DEFAULT '0',
  PRIMARY KEY (`Id`),
  KEY `FK_Character_Stage` (`Stage_Id`),
  CONSTRAINT `FK_Character_Stage` FOREIGN KEY (`Stage_Id`) REFERENCES `Stage` (`Id`) ON DELETE SET NULL ON UPDATE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- Дамп данных таблицы sts.Character: ~3 rows (приблизительно)
/*!40000 ALTER TABLE `Character` DISABLE KEYS */;
REPLACE INTO `Character` (`Id`, `Title`, `Gender`, `Level`, `GameTime`, `GameDay`, `GameScene`, `LocationX`, `LocationY`, `LocationZ`, `Stage_Id`) VALUES
	(1, 'Daniel', 'M', 1, '2015-01-01 00:00:00', '0', 'MainMenu', 970, 54.01, 1383.77, 1),
	(2, 'Rita', 'F', 1, '2015-01-01 00:00:00', '0', 'MainMenu', 970, 54.01, 1383.77, 1),
	(3, 'Tania', 'F', 1, '2015-01-01 00:00:00', '0', 'MainMenu', 970, 54.01, 1383.77, 1);
/*!40000 ALTER TABLE `Character` ENABLE KEYS */;


-- Дамп структуры для таблица sts.Company
CREATE TABLE IF NOT EXISTS `Company` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Title` varchar(50) NOT NULL DEFAULT '0',
  `Share` double NOT NULL DEFAULT '0',
  `Period` int(11) NOT NULL DEFAULT '0',
  `Investment` decimal(10,2) NOT NULL DEFAULT '0.00',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- Дамп данных таблицы sts.Company: ~1 rows (приблизительно)
/*!40000 ALTER TABLE `Company` DISABLE KEYS */;
REPLACE INTO `Company` (`Id`, `Title`, `Share`, `Period`, `Investment`) VALUES
	(1, 'FirstCompany', 0, 0, 0.00);
/*!40000 ALTER TABLE `Company` ENABLE KEYS */;


-- Дамп структуры для таблица sts.Competitor
CREATE TABLE IF NOT EXISTS `Competitor` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Title` varchar(50) NOT NULL DEFAULT '0',
  `Success_rate` double NOT NULL DEFAULT '0',
  `Enterprise_id` bigint(20) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_Competitor_Enterprise` (`Enterprise_id`),
  CONSTRAINT `FK_Competitor_Enterprise` FOREIGN KEY (`Enterprise_id`) REFERENCES `Enterprise` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- Дамп данных таблицы sts.Competitor: ~1 rows (приблизительно)
/*!40000 ALTER TABLE `Competitor` DISABLE KEYS */;
REPLACE INTO `Competitor` (`Id`, `Title`, `Success_rate`, `Enterprise_id`) VALUES
	(1, 'Competitor', 0, 1);
/*!40000 ALTER TABLE `Competitor` ENABLE KEYS */;


-- Дамп структуры для таблица sts.Document
CREATE TABLE IF NOT EXISTS `Document` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Title` varchar(50) NOT NULL DEFAULT '0',
  `Type` varchar(50) NOT NULL DEFAULT '0',
  `Path` varchar(50) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- Дамп данных таблицы sts.Document: ~1 rows (приблизительно)
/*!40000 ALTER TABLE `Document` DISABLE KEYS */;
REPLACE INTO `Document` (`Id`, `Title`, `Type`, `Path`) VALUES
	(1, 'Doc1', '0', '0');
/*!40000 ALTER TABLE `Document` ENABLE KEYS */;


-- Дамп структуры для таблица sts.Employee
CREATE TABLE IF NOT EXISTS `Employee` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Title` varchar(50) NOT NULL,
  `Qualification` double NOT NULL,
  `Salary` decimal(10,2) NOT NULL DEFAULT '0.00',
  `Role_id` bigint(20) DEFAULT '0',
  `Enterprise_id` bigint(20) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  KEY `FK_Employee_Enterprise` (`Enterprise_id`),
  KEY `FK_Employee_Role` (`Role_id`),
  CONSTRAINT `FK_Employee_Enterprise` FOREIGN KEY (`Enterprise_id`) REFERENCES `Enterprise` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_Employee_Role` FOREIGN KEY (`Role_id`) REFERENCES `Role` (`Id`) ON DELETE SET NULL ON UPDATE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- Дамп данных таблицы sts.Employee: ~1 rows (приблизительно)
/*!40000 ALTER TABLE `Employee` DISABLE KEYS */;
REPLACE INTO `Employee` (`Id`, `Title`, `Qualification`, `Salary`, `Role_id`, `Enterprise_id`) VALUES
	(1, 'Jack', 0, 0.00, 1, 1);
/*!40000 ALTER TABLE `Employee` ENABLE KEYS */;


-- Дамп структуры для таблица sts.Enterprise
CREATE TABLE IF NOT EXISTS `Enterprise` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Title` varchar(50) NOT NULL DEFAULT '0',
  `Balance` decimal(10,2) NOT NULL DEFAULT '0.00',
  `Stationary` double NOT NULL DEFAULT '0',
  `Type` smallint(6) DEFAULT '0',
  `Taxation_id` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_Enterprise_Taxation` (`Taxation_id`),
  CONSTRAINT `FK_Enterprise_Character` FOREIGN KEY (`Id`) REFERENCES `Character` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_Enterprise_Taxation` FOREIGN KEY (`Taxation_id`) REFERENCES `Taxation` (`Id`) ON DELETE SET NULL ON UPDATE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- Дамп данных таблицы sts.Enterprise: ~1 rows (приблизительно)
/*!40000 ALTER TABLE `Enterprise` DISABLE KEYS */;
REPLACE INTO `Enterprise` (`Id`, `Title`, `Balance`, `Stationary`, `Type`, `Taxation_id`) VALUES
	(1, 'DEnterprise', 0.00, 0, 0, 1);
/*!40000 ALTER TABLE `Enterprise` ENABLE KEYS */;


-- Дамп структуры для таблица sts.Enterprise_docs
CREATE TABLE IF NOT EXISTS `Enterprise_docs` (
  `Document_id` bigint(20) NOT NULL,
  `Enterprise_id` bigint(20) NOT NULL,
  `Availability` tinyint(1) NOT NULL,
  `Is_active` tinyint(1) NOT NULL,
  `Expiration_date` datetime NOT NULL DEFAULT '2015-01-01 00:00:00',
  PRIMARY KEY (`Document_id`,`Enterprise_id`),
  KEY `FK_Enterprise_docs_Enterprise` (`Enterprise_id`),
  CONSTRAINT `FK_Enterprise_docs_Document` FOREIGN KEY (`Document_id`) REFERENCES `Document` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_Enterprise_docs_Enterprise` FOREIGN KEY (`Enterprise_id`) REFERENCES `Enterprise` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Дамп данных таблицы sts.Enterprise_docs: ~1 rows (приблизительно)
/*!40000 ALTER TABLE `Enterprise_docs` DISABLE KEYS */;
REPLACE INTO `Enterprise_docs` (`Document_id`, `Enterprise_id`, `Availability`, `Is_active`, `Expiration_date`) VALUES
	(1, 1, 1, 0, '2015-01-01 00:00:00');
/*!40000 ALTER TABLE `Enterprise_docs` ENABLE KEYS */;


-- Дамп структуры для таблица sts.Enterprise_equipment
CREATE TABLE IF NOT EXISTS `Enterprise_equipment` (
  `Equipment_id` bigint(20) NOT NULL,
  `Enterprise_id` bigint(20) NOT NULL,
  `Purchase_date` datetime NOT NULL DEFAULT '2015-01-01 00:00:00',
  `Quantity` int(11) DEFAULT NULL,
  `Lease_term` int(11) DEFAULT NULL,
  `IsRunning` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`Equipment_id`,`Enterprise_id`),
  KEY `FK_Enterprise_equipment_Enterprise` (`Enterprise_id`),
  CONSTRAINT `FK_Enterprise_equipment_Enterprise` FOREIGN KEY (`Enterprise_id`) REFERENCES `Enterprise` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_Enterprise_equipment_Equipment` FOREIGN KEY (`Equipment_id`) REFERENCES `Equipment` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Дамп данных таблицы sts.Enterprise_equipment: ~1 rows (приблизительно)
/*!40000 ALTER TABLE `Enterprise_equipment` DISABLE KEYS */;
REPLACE INTO `Enterprise_equipment` (`Equipment_id`, `Enterprise_id`, `Purchase_date`, `Quantity`, `Lease_term`, `IsRunning`) VALUES
	(1, 1, '2015-05-03 21:19:01', NULL, NULL, NULL);
/*!40000 ALTER TABLE `Enterprise_equipment` ENABLE KEYS */;


-- Дамп структуры для таблица sts.Equipment
CREATE TABLE IF NOT EXISTS `Equipment` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Title` varchar(50) NOT NULL DEFAULT '0',
  `Price` decimal(10,2) NOT NULL DEFAULT '0.00',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- Дамп данных таблицы sts.Equipment: ~1 rows (приблизительно)
/*!40000 ALTER TABLE `Equipment` DISABLE KEYS */;
REPLACE INTO `Equipment` (`Id`, `Title`, `Price`) VALUES
	(1, 'Chair', 0.00);
/*!40000 ALTER TABLE `Equipment` ENABLE KEYS */;


-- Дамп структуры для таблица sts.Product
CREATE TABLE IF NOT EXISTS `Product` (
  `Project_id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Title` varchar(50) NOT NULL DEFAULT '0',
  `Price` decimal(10,2) NOT NULL DEFAULT '0.00',
  `Quality` double NOT NULL DEFAULT '0',
  `Prime_cost` decimal(10,2) NOT NULL DEFAULT '0.00',
  PRIMARY KEY (`Project_id`),
  CONSTRAINT `FK_Product_Project` FOREIGN KEY (`Project_id`) REFERENCES `Project` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- Дамп данных таблицы sts.Product: ~1 rows (приблизительно)
/*!40000 ALTER TABLE `Product` DISABLE KEYS */;
REPLACE INTO `Product` (`Project_id`, `Title`, `Price`, `Quality`, `Prime_cost`) VALUES
	(1, 'Product', 0.00, 0, 0.00);
/*!40000 ALTER TABLE `Product` ENABLE KEYS */;


-- Дамп структуры для таблица sts.Project
CREATE TABLE IF NOT EXISTS `Project` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Title` varchar(50) NOT NULL,
  `Planned_begin_date` datetime NOT NULL DEFAULT '2015-01-01 00:00:00',
  `Planned_end_date` datetime NOT NULL DEFAULT '2015-01-01 00:00:00',
  `Real_begin_date` datetime NOT NULL DEFAULT '2015-01-01 00:00:00',
  `Real_end_date` datetime NOT NULL DEFAULT '2015-01-01 00:00:00',
  `State` int(11) NOT NULL DEFAULT '0',
  `Stated_budget` decimal(10,2) NOT NULL DEFAULT '0.00',
  `Expenditures` decimal(10,2) NOT NULL DEFAULT '0.00',
  `Enterprise_id` bigint(20) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  KEY `FK_Project_Enterprise` (`Enterprise_id`),
  CONSTRAINT `FK_Project_Enterprise` FOREIGN KEY (`Enterprise_id`) REFERENCES `Enterprise` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- Дамп данных таблицы sts.Project: ~1 rows (приблизительно)
/*!40000 ALTER TABLE `Project` DISABLE KEYS */;
REPLACE INTO `Project` (`Id`, `Title`, `Planned_begin_date`, `Planned_end_date`, `Real_begin_date`, `Real_end_date`, `State`, `Stated_budget`, `Expenditures`, `Enterprise_id`) VALUES
	(1, 'Proj', '2015-01-01 00:00:00', '2015-01-01 00:00:00', '2015-01-01 00:00:00', '2015-01-01 00:00:00', 0, 0.00, 0.00, 1);
/*!40000 ALTER TABLE `Project` ENABLE KEYS */;


-- Дамп структуры для таблица sts.Project_stage
CREATE TABLE IF NOT EXISTS `Project_stage` (
  `Project_id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Conception_hours` int(11) DEFAULT '0',
  `Programming_hours` int(11) DEFAULT '0',
  `Testing_hours` int(11) DEFAULT '0',
  `Design_hours` int(11) DEFAULT '0',
  `Conception_done` double DEFAULT '0',
  `Programming_done` double DEFAULT '0',
  `Testing_done` double DEFAULT '0',
  `Design_done` double DEFAULT '0',
  PRIMARY KEY (`Project_id`),
  CONSTRAINT `FK_Project_stage_Project` FOREIGN KEY (`Project_id`) REFERENCES `Project` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- Дамп данных таблицы sts.Project_stage: ~1 rows (приблизительно)
/*!40000 ALTER TABLE `Project_stage` DISABLE KEYS */;
REPLACE INTO `Project_stage` (`Project_id`, `Conception_hours`, `Programming_hours`, `Testing_hours`, `Design_hours`, `Conception_done`, `Programming_done`, `Testing_done`, `Design_done`) VALUES
	(1, 0, 0, 0, 0, 0, 0, 0, 0);
/*!40000 ALTER TABLE `Project_stage` ENABLE KEYS */;


-- Дамп структуры для таблица sts.Role
CREATE TABLE IF NOT EXISTS `Role` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Title` varchar(50) NOT NULL DEFAULT '0',
  `Min_salary` decimal(10,2) NOT NULL DEFAULT '0.00',
  `Max_salary` decimal(10,2) NOT NULL DEFAULT '0.00',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- Дамп данных таблицы sts.Role: ~1 rows (приблизительно)
/*!40000 ALTER TABLE `Role` DISABLE KEYS */;
REPLACE INTO `Role` (`Id`, `Title`, `Min_salary`, `Max_salary`) VALUES
	(1, 'Programmer', 0.00, 0.00);
/*!40000 ALTER TABLE `Role` ENABLE KEYS */;


-- Дамп структуры для таблица sts.Salary_payment
CREATE TABLE IF NOT EXISTS `Salary_payment` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Date` datetime NOT NULL DEFAULT '2015-01-01 00:00:00',
  `Hours_worked` int(11) DEFAULT NULL,
  `Salary` int(11) DEFAULT '0',
  `Employee_id` bigint(20) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  KEY `FK_Salary_payment_Employee` (`Employee_id`),
  CONSTRAINT `FK_Salary_payment_Employee` FOREIGN KEY (`Employee_id`) REFERENCES `Employee` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- Дамп данных таблицы sts.Salary_payment: ~1 rows (приблизительно)
/*!40000 ALTER TABLE `Salary_payment` DISABLE KEYS */;
REPLACE INTO `Salary_payment` (`Id`, `Date`, `Hours_worked`, `Salary`, `Employee_id`) VALUES
	(2, '2015-05-03 21:21:25', NULL, 200, 1);
/*!40000 ALTER TABLE `Salary_payment` ENABLE KEYS */;


-- Дамп структуры для таблица sts.Service
CREATE TABLE IF NOT EXISTS `Service` (
  `Company_id` bigint(20) NOT NULL,
  `Enterprise_id` bigint(20) NOT NULL,
  `Title` varchar(50) NOT NULL,
  `Price` decimal(10,2) NOT NULL,
  `Period` int(11) NOT NULL,
  `Periods_paid` int(11) NOT NULL,
  `Effectiveness` double NOT NULL,
  PRIMARY KEY (`Company_id`,`Enterprise_id`),
  KEY `FK_Service_Enterprise` (`Enterprise_id`),
  CONSTRAINT `FK_Service_Company` FOREIGN KEY (`Company_id`) REFERENCES `Company` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_Service_Enterprise` FOREIGN KEY (`Enterprise_id`) REFERENCES `Enterprise` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Дамп данных таблицы sts.Service: ~1 rows (приблизительно)
/*!40000 ALTER TABLE `Service` DISABLE KEYS */;
REPLACE INTO `Service` (`Company_id`, `Enterprise_id`, `Title`, `Price`, `Period`, `Periods_paid`, `Effectiveness`) VALUES
	(1, 1, 'Service', 0.00, 0, 0, 0);
/*!40000 ALTER TABLE `Service` ENABLE KEYS */;


-- Дамп структуры для таблица sts.Stage
CREATE TABLE IF NOT EXISTS `Stage` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Title` varchar(50) DEFAULT NULL,
  `Mission` int(11) NOT NULL DEFAULT '0',
  `TargetX` bigint(20) DEFAULT NULL,
  `TargetY` bigint(20) DEFAULT NULL,
  `TargetZ` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- Дамп данных таблицы sts.Stage: ~1 rows (приблизительно)
/*!40000 ALTER TABLE `Stage` DISABLE KEYS */;
REPLACE INTO `Stage` (`Id`, `Title`, `Mission`, `TargetX`, `TargetY`, `TargetZ`) VALUES
	(1, 'Start', 1, 0, 0, 0);
/*!40000 ALTER TABLE `Stage` ENABLE KEYS */;


-- Дамп структуры для таблица sts.Taxation
CREATE TABLE IF NOT EXISTS `Taxation` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Taxation_group` smallint(6) NOT NULL DEFAULT '0',
  `Max_revenue` decimal(10,2) NOT NULL DEFAULT '0.00',
  `Max_employee` int(11) NOT NULL DEFAULT '0',
  `VAT` double NOT NULL DEFAULT '0',
  `Income_duty` double NOT NULL DEFAULT '0',
  `Type` smallint(6) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- Дамп данных таблицы sts.Taxation: ~1 rows (приблизительно)
/*!40000 ALTER TABLE `Taxation` DISABLE KEYS */;
REPLACE INTO `Taxation` (`Id`, `Taxation_group`, `Max_revenue`, `Max_employee`, `VAT`, `Income_duty`, `Type`) VALUES
	(1, 1, 0.00, 0, 0, 0, 0);
/*!40000 ALTER TABLE `Taxation` ENABLE KEYS */;


-- Дамп структуры для таблица sts.Team_member
CREATE TABLE IF NOT EXISTS `Team_member` (
  `Employee_id` bigint(20) NOT NULL,
  `Project_id` bigint(20) NOT NULL,
  PRIMARY KEY (`Employee_id`,`Project_id`),
  KEY `FK_Team_member_Project` (`Project_id`),
  CONSTRAINT `FK_Team_member_Employee` FOREIGN KEY (`Employee_id`) REFERENCES `Employee` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_Team_member_Project` FOREIGN KEY (`Project_id`) REFERENCES `Project` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Дамп данных таблицы sts.Team_member: ~1 rows (приблизительно)
/*!40000 ALTER TABLE `Team_member` DISABLE KEYS */;
REPLACE INTO `Team_member` (`Employee_id`, `Project_id`) VALUES
	(1, 1);
/*!40000 ALTER TABLE `Team_member` ENABLE KEYS */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
