CREATE TABLE 	`ijerts`.`queries` (
  `QueryId` int(11) NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(255) DEFAULT NULL,
  `LastName` varchar(255) DEFAULT NULL,
  `Email` varchar(255) DEFAULT NULL,
  `QueryText` varchar(1000) DEFAULT NULL,
  `QueryAnswer` varchar(1000) DEFAULT NULL,
  `QueryStatus` varchar(500) DEFAULT NULL,
  `IsActive` int(11) DEFAULT NULL,
  `CreatedBy` varchar(255) DEFAULT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(255) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
   PRIMARY KEY (`QueryId`)

  ) ENGINE=InnoDB DEFAULT CHARSET=latin1;
