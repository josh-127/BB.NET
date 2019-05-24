
CREATE DATABASE IF NOT EXISTS `BBNet`
    CHARACTER SET = 'utf8mb4'
    COLLATE = 'utf8mb4_0900_ai_ci';

USE BBNet;

CREATE TABLE IF NOT EXISTS `Community` (
    `Id`            INT             NOT NULL    AUTO_INCREMENT  PRIMARY KEY,
    `Name`          VARCHAR(79)     NOT NULL,
    `Description`   VARCHAR(255)    NOT NULL,
    `Created`       TIMESTAMP       NOT NULL    DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS `Forum` (
    `Id`            INT             NOT NULL    AUTO_INCREMENT  PRIMARY KEY,
    `Name`          VARCHAR(79)     NOT NULL,
    `Description`   VARCHAR(255)    NOT NULL,
    `ImageUrl`      VARCHAR(255)    NOT NULL,
    `Created`       TIMESTAMP       NOT NULL    DEFAULT CURRENT_TIMESTAMP,
    `CommunityId`   INT             NOT NULL    REFERENCES `Community`
);

CREATE TABLE IF NOT EXISTS `Topic` (
    `Id`            INT             NOT NULL    AUTO_INCREMENT  PRIMARY KEY,
    `Name`          VARCHAR(79)     NOT NULL,
    `Description`   VARCHAR(79)     NOT NULL,
    `Created`       TIMESTAMP       NOT NULL    DEFAULT CURRENT_TIMESTAMP,
    `ForumId`       INT             NOT NULL    REFERENCES `Forum`
);

DELIMITER $$
CREATE PROCEDURE CreateCommunity(IN `name` VARCHAR(79), IN `description` VARCHAR(255))
BEGIN
    INSERT INTO `Community` (`Name`, `Description`) VALUES (`name`, `description`);
END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE GetCommunityListings()
BEGIN
    SELECT `Id`, `Name`, `Description`, `Created` FROM `Community`;
END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE CreateForum(
    IN `name` VARCHAR(79),
    IN `description` VARCHAR(255),
    IN `imageUrl` VARCHAR(255),
    IN `communityId` INT)
BEGIN
    INSERT INTO `Forum` (`Name`, `Description`, `ImageUrl`, `CommunityId`)
        VALUES (`name`, `description`, `imageUrl`, `communityId`);
END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE GetForumIndex(IN `communityId` INT)
BEGIN
    SELECT `Name`, `Description`
        FROM `Community`
        WHERE `Id` = `communityId`;

    SELECT `Id`, `Name`, `Description`, `ImageUrl`
        FROM `Forum`
        WHERE `CommunityId` = `communityId`;
END $$
DELIMITER ;