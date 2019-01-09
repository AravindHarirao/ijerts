CREATE TABLE `approvers` (
  `ApproverId` bigint(11) NOT NULL AUTO_INCREMENT,
  `ApproverFirstName` varchar(100) NOT NULL,
  `ApproverLastName` varchar(100) NOT NULL,
  `ApproverDept` varchar(100) NOT NULL,
  `ApproverEmail` varchar(100) NOT NULL,
  `ApproverMobile` varchar(50) NOT NULL,
  `CreatedDatetime` datetime DEFAULT NULL,
  `CreatedBy` varchar(100) DEFAULT NULL,
  `UpdatedDatetime` datetime DEFAULT NULL,
  `UpdatedBy` varchar(100) DEFAULT NULL,
  `IsActive` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`ApproverId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

CREATE TABLE `authors` (
  `AuthorId` int(11) NOT NULL AUTO_INCREMENT,
  `PaperId` int(11) DEFAULT NULL,
  `AuthorFirstName` varchar(255) DEFAULT NULL,
  `AuthorLastName` varchar(255) DEFAULT NULL,
  `AuthorDepartment` varchar(255) DEFAULT NULL,
  `AuthorOrganisation` varchar(255) DEFAULT NULL,
  `AuthorSubject` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`AuthorId`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=latin1;

CREATE TABLE `commoncode` (
  `Id` int(11) NOT NULL,
  `GroupId` int(11) NOT NULL,
  `GroupName` varchar(50) NOT NULL,
  `CodeName` varchar(50) NOT NULL,
  `SortOrder` int(11) NOT NULL,
  `IsActive` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `login` (
  `userid` varchar(255) DEFAULT NULL,
  `password` varchar(1000) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `papers` (
  `PaperId` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` int(11) NOT NULL,
  `MainTitle` varchar(255) DEFAULT NULL,
  `ShortDesc` varchar(1000) DEFAULT NULL,
  `Subject` varchar(255) DEFAULT NULL,
  `Tags` varchar(255) DEFAULT NULL,
  `CompleteFilePath` varchar(1000) DEFAULT NULL,
  `FileName` varchar(255) DEFAULT NULL,
  `CreatedBy` varchar(255) DEFAULT NULL,
  `CreatedDateTime` datetime DEFAULT NULL,
  `UpdatedBy` varchar(255) DEFAULT NULL,
  `UpdatedDateTime` datetime DEFAULT NULL,
  PRIMARY KEY (`PaperId`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=latin1;

CREATE TABLE `paperstatus` (
  `StatusId` int(11) NOT NULL AUTO_INCREMENT,
  `PaperId` int(11) NOT NULL,
  `UserId` int(11) NOT NULL,
  `Status` varchar(255) DEFAULT NULL,
  `CreatedBy` varchar(255) DEFAULT NULL,
  `CreatedDateTime` datetime DEFAULT NULL,
  PRIMARY KEY (`StatusId`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;


CREATE TABLE `useraddress` (
  `AddressId` int(11) NOT NULL AUTO_INCREMENT,
  `AddressLine1` varchar(150) NOT NULL,
  `AddressLine2` varchar(150) DEFAULT NULL,
  `City` varchar(75) DEFAULT NULL,
  `State` varchar(75) DEFAULT NULL,
  `Country` varchar(75) DEFAULT NULL,
  `PostalCode` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`AddressId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `userloginhistory` (
  `LoginId` bigint(11) NOT NULL AUTO_INCREMENT,
  `UserId` bigint(11) NOT NULL,
  `SessionId` varchar(50) NOT NULL,
  `LoggedInTime` datetime NOT NULL,
  `LoggedOutTime` datetime DEFAULT NULL,
  `IsActive` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`LoginId`)
) ENGINE=InnoDB AUTO_INCREMENT=39 DEFAULT CHARSET=latin1;

CREATE TABLE `users` (
  `UserId` int(11) NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(255) DEFAULT NULL,
  `LastName` varchar(255) DEFAULT NULL,
  `Email` varchar(255) DEFAULT NULL,
  `Phone` varchar(50) DEFAULT NULL,
  `Password` varchar(500) DEFAULT NULL,
  `Organisation` varchar(150) DEFAULT NULL,
  `Qualification` varchar(150) DEFAULT NULL,
  `Position` varchar(150) DEFAULT NULL,
  `AddressId` int(11) DEFAULT NULL,
  `SpecializationGroupId` int(11) DEFAULT NULL,
  `SpecializationId` int(11) DEFAULT NULL,
  `UserType` char(1) NOT NULL COMMENT 'A - Author, E- Editor',
  `UserActivationValue` varchar(50) DEFAULT NULL,
  `UserActivated` bit(1) DEFAULT b'0',
  `IsActive` int(11) DEFAULT NULL,
  `CreatedDateTime` datetime DEFAULT NULL,
  `CreatedBy` varchar(255) DEFAULT NULL,
  `UpdatedDatetime` varchar(255) DEFAULT NULL,
  `UpdatedBy` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;





