
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