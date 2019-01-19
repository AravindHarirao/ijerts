CREATE TABLE `ijerts`.Queries
(
QueryId INT,
FirstName varchar(255),
LastName varchar(255),
Email varchar(255),
QueryText VARCHAR(1000),
QueryStatus VARCHAR(500),
IsActive INT,
CreatedBy VARCHAR(255),
CreatedOn DATETIME
);
Commit;

INSERT INTO `ijerts`.Queries (FirstName, LastName, Email, QueryText, QueryStatus, IsActive, CreatedBy, CreatedOn)
VALUES
('Aravind', 'Harirao', 'h.aravind@gmail.com', 'Sample Query', 'New', 1, 'System', now()
);
commit;

select * from `ijerts`.Queries