ALTER TABLE `ijerts`.`users` CHANGE COLUMN `UserType` `UserType` CHAR(1) NOT NULL COMMENT 'A - Author, E- Editor, R - Reviewer' ;

ALTER TABLE `ijerts`.`users` 
ADD COLUMN `Department` VARCHAR(150) NULL AFTER `Position`,
ADD COLUMN `ResumeFileName` VARCHAR(150) NULL AFTER `UserActivated`;
