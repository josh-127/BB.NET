
CREATE SCHEMA IF NOT EXISTS `PicoBoards`
    DEFAULT CHARACTER SET utf8mb4
    DEFAULT COLLATE utf8mb4_0900_ai_ci;

USE `PicoBoards`;

CREATE TABLE IF NOT EXISTS `Category` (
    `CategoryId`    INT         NOT NULL    AUTO_INCREMENT,
    `Name`          VARCHAR(45),
    PRIMARY KEY (`CategoryId`)
);

CREATE TABLE IF NOT EXISTS `Forum` (
    `ForumId`       INT             NOT NULL    AUTO_INCREMENT,
    `CategoryId`    INT             NOT NULL,
    `Name`          VARCHAR(45)     NOT NULL,
    `Description`   VARCHAR(255),
    `ImageUrl`      VARCHAR(255),
    `Created`       TIMESTAMP       NOT NULL    DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (`ForumId`),
    INDEX `idx_Forum_CategoryId` (`CategoryId`),
    CONSTRAINT `fk_Forum_CategoryId`
        FOREIGN KEY (`CategoryId`)
        REFERENCES `Category` (`CategoryId`)
);

CREATE TABLE IF NOT EXISTS `Group` (
    `GroupId`       INT             NOT NULL    AUTO_INCREMENT,
    `Name`          VARCHAR(45)     NOT NULL,
    `Description`   VARCHAR(255),
    PRIMARY KEY (`GroupId`),
    CONSTRAINT `uk_Group_Name`
        UNIQUE KEY (`Name`)
);

CREATE TABLE IF NOT EXISTS `User` (
    `UserId`            INT             NOT NULL    AUTO_INCREMENT,
    `GroupId`           INT             NOT NULL,
    `EmailAddress`      VARCHAR(45)     NOT NULL,
    `UserName`          VARCHAR(45)     NOT NULL,
    `Password`          VARCHAR(45)     NOT NULL,
    `Created`           TIMESTAMP       NOT NULL    DEFAULT CURRENT_TIMESTAMP,
    `LastActive`        TIMESTAMP       NOT NULL    DEFAULT CURRENT_TIMESTAMP   ON UPDATE CURRENT_TIMESTAMP,
    `Birthday`          DATE,
    `Location`          VARCHAR(45),
    `Signature`         TEXT,
    PRIMARY KEY (`UserId`),
    INDEX `idx_User_GroupId` (`GroupId`),
    CONSTRAINT `uk_User_UserName`
        UNIQUE KEY (`UserName`),
    CONSTRAINT `fk_User_GroupId`
        FOREIGN KEY (`GroupId`)
        REFERENCES `Group` (`GroupId`)
);

CREATE TABLE IF NOT EXISTS `Topic` (
    `TopicId`       INT             NOT NULL    AUTO_INCREMENT,
    `ForumId`       INT             NOT NULL,
    `Name`          VARCHAR(45)     NOT NULL,
    `Description`   VARCHAR(255),
    `Created`       TIMESTAMP       NOT NULL    DEFAULT CURRENT_TIMESTAMP,
    `IsLocked`      TINYINT         NOT NULL    DEFAULT 0,
    `IsSticky`      TINYINT         NOT NULL    DEFAULT 0,
    PRIMARY KEY (`TopicId`),
    INDEX `idx_Topic_ForumId` (`ForumId`),
    CONSTRAINT `fk_Topic_ForumId`
        FOREIGN KEY (`ForumId`)
        REFERENCES `Forum` (`ForumId`)
);

CREATE TABLE IF NOT EXISTS `Post` (
    `PostId`            INT         NOT NULL    AUTO_INCREMENT,
    `TopicId`           INT         NOT NULL,
    `UserId`            INT         NOT NULL,
    `Name`              VARCHAR(45) NOT NULL,
    `Body`              MEDIUMTEXT  NOT NULL,
    `Created`           TIMESTAMP   NOT NULL    DEFAULT CURRENT_TIMESTAMP,
    `Modified`          TIMESTAMP   NOT NULL    DEFAULT CURRENT_TIMESTAMP,
    `FormattingEnabled` TINYINT     NOT NULL    DEFAULT 1,
    `SmiliesEnabled`    TINYINT     NOT NULL    DEFAULT 1,
    `ParseUrls`         TINYINT     NOT NULL    DEFAULT 1,
    `AttachSignature`   TINYINT     NOT NULL    DEFAULT 1,
    PRIMARY KEY (`PostId`),
    INDEX `idx_Post_TopicId` (`TopicId`),
    INDEX `idx_Post_UserId` (`UserId`),
    CONSTRAINT `fk_Post_TopicId`
        FOREIGN KEY (`TopicId`)
        REFERENCES `Topic` (`TopicId`),
    CONSTRAINT `fk_Post_UserId`
        FOREIGN KEY (`UserId`)
        REFERENCES `User` (`UserId`)
);

CREATE TABLE IF NOT EXISTS `Attachment` (
    `AttachmentId`  INT         NOT NULL    AUTO_INCREMENT,
    `PostId`        INT         NOT NULL,
    `FileName`      VARCHAR(45) NOT NULL,
    `Contents`      MEDIUMBLOB  NOT NULL,
    PRIMARY KEY (`AttachmentId`),
    INDEX `idx_Attachment_PostId` (`PostId`),
    CONSTRAINT `fk_Attachment_PostId`
        FOREIGN KEY (`PostId`)
        REFERENCES `Post` (`PostId`)
);

CREATE TABLE IF NOT EXISTS `Poll` (
    `PollId`        INT             NOT NULL    AUTO_INCREMENT,
    `TopicId`       INT             NOT NULL,
    `Prompt`        VARCHAR(255)    NOT NULL,
    `Expiration`    DATETIME,
    `MaxChoices`    INT             NOT NULL,
    PRIMARY KEY (`PollId`),
    INDEX `idx_Poll_TopicId` (`TopicId`),
    CONSTRAINT `fk_Poll_TopicId`
        FOREIGN KEY (`TopicId`)
        REFERENCES `Topic` (`TopicId`)
);

CREATE TABLE IF NOT EXISTS `PollChoice` (
    `PollChoiceId`  INT             NOT NULL    AUTO_INCREMENT,
    `PollId`        INT             NOT NULL,
    `Text`          VARCHAR(255)    NOT NULL,
    PRIMARY KEY (`PollChoiceId`),
    INDEX `idx_PollChoice_PollId` (`PollId`),
    CONSTRAINT `fk_PollChoice_PollId`
        FOREIGN KEY (`PollId`)
        REFERENCES `Poll` (`PollId`)
);

CREATE TABLE IF NOT EXISTS `PollVote` (
    `PollChoiceId`  INT NOT NULL,
    `UserId`        INT NOT NULL,
    PRIMARY KEY (`PollChoiceId`, `UserId`),
    CONSTRAINT `fk_PollVote_PollChoiceId`
        FOREIGN KEY (`PollChoiceId`)
        REFERENCES `PollChoice` (`PollChoiceId`),
    CONSTRAINT `fk_PollVote_UserId`
        FOREIGN KEY (`UserId`)
        REFERENCES `User` (`UserId`)
);

CREATE TABLE IF NOT EXISTS `GlobalConfiguration` (
    `GlobalConfigurationId` ENUM("0")   NOT NULL    DEFAULT "0",
    `DefaultGroupId`        INT         NOT NULL,
    PRIMARY KEY (`GlobalConfigurationId`),
    CONSTRAINT `fk_GlobalConfiguration_DefaultGroupId`
        FOREIGN KEY (`DefaultGroupId`)
        REFERENCES `Group` (`GroupId`)
);

INSERT INTO `Group`
    (`GroupId`, `Name`, `Description`)
    VALUES
    (1, "Administrator", NULL),
    (2, "Global Moderator", NULL),
    (3, "Member", NULL);

INSERT INTO `User`
    (`UserId`, `GroupId`, `EmailAddress`, `UserName`, `Password`)
    VALUES
    (1, 1, "admin@example.com", "admin", "password");

INSERT INTO `GlobalConfiguration`
    (`DefaultGroupId`) VALUES (3);