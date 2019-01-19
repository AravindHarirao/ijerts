CREATE TABLE `ijerts`.`papersApprovers` (
  `PaperApproverId` int(11) NOT NULL AUTO_INCREMENT,
  `PaperId` INT NOT NULL,
  `ApproverId` INT NOT NULL,
  `IsActive` int(11) DEFAULT NULL,
  `CreatedDateTime` datetime DEFAULT NULL,
  `CreatedBy` varchar(255) DEFAULT NULL,
  `UpdatedDatetime` varchar(255) DEFAULT NULL,
  `UpdatedBy` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`PaperApproverId`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=latin1;
Commit;