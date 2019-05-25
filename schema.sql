
CREATE DATABASE IF NOT EXISTS `BBNet`
    CHARACTER SET = 'utf8mb4'
    COLLATE = 'utf8mb4_0900_ai_ci';

USE BBNet;

CREATE TABLE IF NOT EXISTS `Category` (
    `Id`            INT             NOT NULL    AUTO_INCREMENT  PRIMARY KEY,
    `Name`          VARCHAR(79)     NOT NULL
);

CREATE TABLE IF NOT EXISTS `Forum` (
    `Id`            INT             NOT NULL    AUTO_INCREMENT  PRIMARY KEY,
    `CategoryId`    INT             NOT NULL    REFERENCES `Category`,
    `Name`          VARCHAR(79)     NOT NULL,
    `Description`   VARCHAR(255),
    `ImageUrl`      VARCHAR(255),
    `Created`       TIMESTAMP       NOT NULL    DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS `Topic` (
    `Id`            INT             NOT NULL    AUTO_INCREMENT  PRIMARY KEY,
    `ForumId`       INT             NOT NULL    REFERENCES `Forum`,
    `Name`          VARCHAR(79)     NOT NULL,
    `Description`   VARCHAR(255),
    `TopicBannerId` INT                         REFERENCES `TopicBanner`,
    `Created`       TIMESTAMP       NOT NULL    DEFAULT CURRENT_TIMESTAMP,
    `IsLocked`      BOOL            NOT NULL,
    `IsSticky`      BOOL            NOT NULL,
    `Views`         INT             NOT NULL
);

CREATE TABLE IF NOT EXISTS `TopicBanner` (
    `Id`            INT             NOT NULL    AUTO_INCREMENT  PRIMARY KEY,
    `ImageUrl`      VARCHAR(255)    NOT NULL
);

CREATE TABLE IF NOT EXISTS `Post` (
    `Id`            INT             NOT NULL    AUTO_INCREMENT  PRIMARY KEY,
    `TopicId`       INT             NOT NULL    REFERENCES `Topic`,
    `Name`          VARCHAR(79)     NOT NULL,
    `Body`          TEXT            NOT NULL,
    `Created`       TIMESTAMP       NOT NULL    DEFAULT CURRENT_TIMESTAMP,
    `Modified`      TIMESTAMP       NOT NULL    DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS `Group` (
    `Id`            INT             NOT NULL    AUTO_INCREMENT  PRIMARY KEY,
    `Name`          VARCHAR(79)     NOT NULL,
    `Description`   VARCHAR(255)
);

CREATE TABLE IF NOT EXISTS `User` (
    `Id`            INT             NOT NULL    AUTO_INCREMENT  PRIMARY KEY,
    `GroupId`       INT             NOT NULL    REFERENCES `Group`,
    `Email`         VARCHAR(79)     NOT NULL,
    `UserName`      VARCHAR(79)     NOT NULL
);

CREATE TABLE IF NOT EXISTS `GroupPermission` (
    `Id`            INT             NOT NULL    AUTO_INCREMENT  PRIMARY KEY,
    `GroupId`       INT             NOT NULL    REFERENCES `Group`
#    `Type`          ENUM            NOT NULL,
#    `Policy`        ENUM            NOT NULL
);

CREATE TABLE IF NOT EXISTS `UserPermission` (
    `Id`            INT             NOT NULL    AUTO_INCREMENT  PRIMARY KEY,
    `UserId`        INT             NOT NULL    REFERENCES `User`
#    `Type`          ENUM            NOT NULL,
#    `Policy`        ENUM            NOT NULL
);

CREATE TABLE IF NOT EXISTS `ForumGroupPermission` (
    `Id`            INT             NOT NULL    AUTO_INCREMENT  PRIMARY KEY,
    `ForumId`       INT             NOT NULL    REFERENCES `Forum`,
    `GroupId`       INT             NOT NULL    REFERENCES `Group`
#    `Type`          ENUM            NOT NULL,
#    `Policy`        ENUM            NOT NULL
);

CREATE TABLE IF NOT EXISTS `ForumUserPermission` (
    `Id`            INT             NOT NULL    AUTO_INCREMENT  PRIMARY KEY,
    `ForumId`       INT             NOT NULL    REFERENCES `Forum`,
    `UserId`        INT             NOT NULL    REFERENCES `User`
#    `Type`          ENUM            NOT NULL,
#    `Policy`        ENUM            NOT NULL
);

CREATE TABLE IF NOT EXISTS `UnreadTopic` (
    `TopicId`       INT             NOT NULL    REFERENCES `Topic`,
    `UserId`        INT             NOT NULL    REFERENCES `User`
);

CREATE TABLE IF NOT EXISTS `PostLike` (
    `PostId`        INT             NOT NULL    REFERENCES `Post`,
    `UserId`        INT             NOT NULL    REFERENCES `User`
);