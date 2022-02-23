CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';


CREATE TABLE IF NOT EXISTS groups(
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'Group Name',
  description varchar(255) COMMENT 'Group Description',
  imgUrl TEXT COMMENT 'User Picture',
  creatorId VARCHAR(255) NOT NULL COMMENT 'Creator Id from Creators Table',

  FOREIGN KEY (creatorId)
    REFERENCES accounts(id)
    ON DELETE CASCADE


) default charset utf8 COMMENT '';
-- TODO add isPrivate

INSERT INTO groups 
(name, description, imgUrl, creatorId)
VALUES
("Marble Gang", "This is how we Roll!", "https://thiscatdoesnotexist.com", "62166d0af004c6f300ee162c");


CREATE TABLE IF NOT EXISTS groupmembers(
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  accountId VARCHAR(255) NOT NULL COMMENT 'Creator Id from Creators Table',
  groupId INT NOT NULL COMMENT 'Group Id',

  FOREIGN KEY (accountId)
    REFERENCES accounts(id)
    ON DELETE CASCADE,

  FOREIGN KEY (groupId)
    REFERENCES groups(id)
    ON DELETE CASCADE


) default charset utf8 COMMENT '';