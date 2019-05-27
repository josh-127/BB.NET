
CREATE SCHEMA IF NOT EXISTS `BBNet`
    DEFAULT CHARACTER SET utf8mb4
    DEFAULT COLLATE utf8mb4_0900_ai_ci;

USE `BBNet`;

CREATE TABLE IF NOT EXISTS `Category` (
    `CategoryId`    INT         NOT NULL    AUTO_INCREMENT,
    `Name`          VARCHAR(45),
    PRIMARY KEY (`CategoryId`)
);

CREATE TABLE IF NOT EXISTS `Forum` (
    `ForumId`       INT         NOT NULL    AUTO_INCREMENT,
    `CategoryId`   INT         NOT NULL,
    `Name`          VARCHAR(45) NOT NULL,
    `Description`   TINYTEXT,
    `ImageUrl`      TINYTEXT,
    `Created`       TIMESTAMP   NOT NULL    DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (`ForumId`),
    INDEX `idx_Forum_CategoryId` (`CategoryId`),
    CONSTRAINT `fk_Forum_CategoryId`
        FOREIGN KEY (`CategoryId`)
        REFERENCES `Category` (`CategoryId`)
);

CREATE TABLE IF NOT EXISTS `TopicBanner` (
    `TopicBannerId` INT         NOT NULL    AUTO_INCREMENT,
    `ImageUrl`      VARCHAR(45) NOT NULL,
    PRIMARY KEY (`TopicBannerId`)
);

CREATE TABLE IF NOT EXISTS `Topic` (
    `TopicId`       INT         NOT NULL    AUTO_INCREMENT,
    `ForumId`       INT         NOT NULL,
    `Name`          VARCHAR(45) NOT NULL,
    `Description`   VARCHAR(45),
    `TopicBannerId` INT,
    `Created`       TIMESTAMP   NOT NULL    DEFAULT CURRENT_TIMESTAMP,
    `IsLocked`      TINYINT     NOT NULL,
    `IsSticky`      TINYINT     NOT NULL,
    PRIMARY KEY (`TopicId`),
    INDEX `idx_Topic_ForumId` (`ForumId`),
    INDEX `idx_Topic_TopicBannerId` (`TopicBannerId`),
    CONSTRAINT `fk_Topic_ForumId`
        FOREIGN KEY (`ForumId`)
        REFERENCES `Forum` (`ForumId`),
    CONSTRAINT `fk_Topic_TopicBannerId`
        FOREIGN KEY (`TopicBannerId`)
        REFERENCES `TopicBanner` (`TopicBannerId`)
);

CREATE TABLE IF NOT EXISTS `Group` (
    `GroupId`       INT         NOT NULL    AUTO_INCREMENT,
    `Name`          VARCHAR(45) NOT NULL,
    `Description`   VARCHAR(45),
    PRIMARY KEY (`GroupId`)
);

CREATE TABLE IF NOT EXISTS `Gender` (
    `GenderId`      INT             NOT NULL    AUTO_INCREMENT,
    `Name`          VARCHAR(45)     NOT NULL,
    `ImageUrl`      VARCHAR(45),
    PRIMARY KEY (`GenderId`)
);

CREATE TABLE IF NOT EXISTS `Country` (
    `CountryId`     INT             NOT NULL    AUTO_INCREMENT,
    `Name`          VARCHAR(45)     NOT NULL,
    `ImageUrl`      VARCHAR(45),
    PRIMARY KEY (`CountryId`)
);

CREATE TABLE IF NOT EXISTS `ProfileImage` (
    `ProfileImageId`    INT         NOT NULL    AUTO_INCREMENT,
    `FileName`          VARCHAR(45) NOT NULL,
    `ImageUrl`          VARCHAR(45) NOT NULL,
    PRIMARY KEY (`ProfileImageId`)
);

CREATE TABLE IF NOT EXISTS `User` (
    `UserId`            INT         NOT NULL    AUTO_INCREMENT,
    `GroupId`           INT         NOT NULL,
    `Email`             VARCHAR(45) NOT NULL,
    `UserName`          VARCHAR(45) NOT NULL,
    `Password`          VARCHAR(45) NOT NULL,
    `GenderId`          INT,
    `CountryId`         INT,
    `ProfileImageId`    INT         NOT NULL,
    `Birthday`          DATE,
    `Interests`         MEDIUMTEXT,
    `Occupation`        MEDIUMTEXT,
    `Location`          VARCHAR(45),
    `Signature`         MEDIUMTEXT,
    PRIMARY KEY (`UserId`),
    INDEX `idx_User_GroupId` (`GroupId`),
    INDEX `idx_User_GenderId` (`GenderId`),
    INDEX `idx_User_CountryId` (`CountryId`),
    INDEX `idx_User_ProfileImageId` (`ProfileImageId`),
    CONSTRAINT `fk_User_GroupId`
        FOREIGN KEY (`GroupId`)
        REFERENCES `Group` (`GroupId`),
    CONSTRAINT `fk_User_GenderId`
        FOREIGN KEY (`GenderId`)
        REFERENCES `Gender` (`GenderId`),
    CONSTRAINT `fk_User_CountryId`
        FOREIGN KEY (`CountryId`)
        REFERENCES `Country` (`CountryId`),
    CONSTRAINT `fk_User_ProfileImageId`
        FOREIGN KEY (`ProfileImageId`)
        REFERENCES `ProfileImage` (`ProfileImageId`)
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

CREATE TABLE IF NOT EXISTS `Capability` (
    `CapabilityId`  VARCHAR(19) NOT NULL,
    `Description`   MEDIUMTEXT,
    PRIMARY KEY (`CapabilityId`)
);

CREATE TABLE IF NOT EXISTS `GroupPermission` (
    `GroupId`       INT         NOT NULL,
    `CapabilityId`  VARCHAR(19) NOT NULL,
    PRIMARY KEY (`GroupId`, `CapabilityId`),
    INDEX `idx_GroupPermission_CapabilityId` (`CapabilityId`),
    CONSTRAINT `fk_GroupPermission_GroupId`
        FOREIGN KEY (`GroupId`)
        REFERENCES `Group` (`GroupId`),
    CONSTRAINT `fk_GroupPermission_CapabilityId`
        FOREIGN KEY (`CapabilityId`)
        REFERENCES `Capability` (`CapabilityId`)
);

CREATE TABLE IF NOT EXISTS `UserPermission` (
    `UserId`        INT         NOT NULL,
    `CapabilityId`  VARCHAR(19) NOT NULL,
    PRIMARY KEY (`UserId`, `CapabilityId`),
    INDEX `idx_UserPermission_CapabilityId` (`CapabilityId`),
    CONSTRAINT `fk_UserPermission_UserId`
        FOREIGN KEY (`UserId`)
        REFERENCES `User` (`UserId`),
    CONSTRAINT `fk_UserPermission_CapabilityId`
        FOREIGN KEY (`CapabilityId`)
        REFERENCES `Capability` (`CapabilityId`)
);

CREATE TABLE IF NOT EXISTS `Poll` (
    `PollId`        INT         NOT NULL    AUTO_INCREMENT,
    `TopicId`       INT         NOT NULL,
    `Prompt`        VARCHAR(45) NOT NULL,
    `Expiration`    DATETIME,
    `MaxChoices`    INT         NOT NULL,
    PRIMARY KEY (`PollId`),
    INDEX `idx_Poll_TopicId` (`TopicId`),
    CONSTRAINT `fk_Poll_TopicId`
        FOREIGN KEY (`TopicId`)
        REFERENCES `Topic` (`TopicId`)
);

CREATE TABLE IF NOT EXISTS `PollChoice` (
    `PollChoiceId`  INT         NOT NULL    AUTO_INCREMENT,
    `PollId`        INT         NOT NULL,
    `Text`          VARCHAR(45) NOT NULL,
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

CREATE TABLE IF NOT EXISTS `ForumCapability` (
    `ForumCapabilityId` VARCHAR(19) NOT NULL,
    `Description`       TEXT,
    PRIMARY KEY (`ForumCapabilityId`)
);

CREATE TABLE IF NOT EXISTS `ForumGroupPermission` (
    `ForumId`           INT         NOT NULL,
    `GroupId`           INT         NOT NULL,
    `ForumCapabilityId` VARCHAR(19) NOT NULL,
    PRIMARY KEY (`ForumId`, `GroupId`, `ForumCapabilityId`),
    INDEX `idx_ForumGroupPermission_group_id` (`GroupId`),
    INDEX `idx_ForumGroupPermission_forum_capability_id` (`ForumCapabilityId`),
    CONSTRAINT `fk_ForumGroupPermission_ForumId`
        FOREIGN KEY (`ForumId`)
        REFERENCES `Forum` (`ForumId`),
    CONSTRAINT `fk_ForumGroupPermission_GroupId`
        FOREIGN KEY (`GroupId`)
        REFERENCES `Group` (`GroupId`),
    CONSTRAINT `fk_ForumGroupPermission_ForumCapabilityId`
        FOREIGN KEY (`ForumCapabilityId`)
        REFERENCES `ForumCapability` (`ForumCapabilityId`)
);

CREATE TABLE IF NOT EXISTS `DefaultForumGroupPermission` (
    `GroupId`           INT         NOT NULL,
    `ForumCapabilityId` VARCHAR(19) NOT NULL,
    PRIMARY KEY (`GroupId`, `ForumCapabilityId`),
    CONSTRAINT `fk_DefaultForumGroupPermission_GroupId`
        FOREIGN KEY (`GroupId`)
        REFERENCES `Group` (`GroupId`),
    CONSTRAINT `fk_DefaultForumGroupPermission_ForumCapabilityId`
        FOREIGN KEY (`ForumCapabilityId`)
        REFERENCES `ForumCapability` (`ForumCapabilityId`)
);

CREATE TABLE IF NOT EXISTS `ForumUserPermission` (
    `ForumId`           INT         NOT NULL,
    `UserId`            INT         NOT NULL,
    `ForumCapabilityId` VARCHAR(19) NOT NULL,
    PRIMARY KEY (`ForumId`, `UserId`, `ForumCapabilityId`),
    INDEX `idx_ForumUserPermission_UserId` (`UserId`),
    INDEX `idx_ForumUserPermission_ForumCapabilityId` (`ForumCapabilityId`),
    CONSTRAINT `fk_ForumUserPermission_ForumId`
        FOREIGN KEY (`ForumId`)
        REFERENCES `Forum` (`ForumId`),
    CONSTRAINT `fk_ForumUserPermission_UserId`
        FOREIGN KEY (`UserId`)
        REFERENCES `User` (`UserId`),
    CONSTRAINT `fk_ForumUserPermission_ForumCapabilityId`
        FOREIGN KEY (`ForumCapabilityId`)
        REFERENCES `ForumCapability` (`ForumCapabilityId`)
);

CREATE TABLE IF NOT EXISTS `DefaultForumUserPermission` (
    `UserId`            INT         NOT NULL,
    `ForumCapabilityId` VARCHAR(19) NOT NULL,
    PRIMARY KEY (`UserId`, `ForumCapabilityId`),
    CONSTRAINT `fk_DefaultForumUserPermission_UserId`
        FOREIGN KEY (`UserId`)
        REFERENCES `User` (`UserId`),
    CONSTRAINT `fk_DefaultForumUserPermission_ForumCapabilityId`
        FOREIGN KEY (`ForumCapabilityId`)
        REFERENCES `ForumCapability` (`ForumCapabilityId`)
);

CREATE TABLE IF NOT EXISTS `PostLike` (
    `PostId`    INT NOT NULL,
    `UserId`    INT NOT NULL,
    PRIMARY KEY (`PostId`, `UserId`),
    INDEX `idx_PostLike_UserId` (`UserId`),
    CONSTRAINT `fk_PostLike_PostId`
        FOREIGN KEY (`PostId`)
        REFERENCES `Post` (`PostId`),
    CONSTRAINT `fk_PostLike_UserId`
        FOREIGN KEY (`UserId`)
        REFERENCES `User` (`UserId`)
);

CREATE TABLE IF NOT EXISTS `UnreadTopic` (
    `TopicId`   INT NOT NULL,
    `UserId`    INT NOT NULL,
    PRIMARY KEY (`TopicId`, `UserId`),
    INDEX `idx_UnreadTopic_UserId` (`UserId`),
    CONSTRAINT `fk_UnreadTopic_TopicId`
        FOREIGN KEY (`TopicId`)
        REFERENCES `Topic` (`TopicId`),
    CONSTRAINT `fk_UnreadTopic_UserId`
        FOREIGN KEY (`UserId`)
        REFERENCES `User` (`UserId`)
);

INSERT INTO `Group`
    (`Name`, `Description`)
    VALUES
    ("Administrator", NULL),
    ("Global Moderator", NULL),
    ("Member", NULL);

INSERT INTO `Gender`
    (`Name`, `ImageUrl`)
    VALUES
    ("Male", NULL),
    ("Female", NULL),
    ("Other", NULL);

INSERT INTO `Country`
    (`Name`, `ImageUrl`)
    VALUES
    ("Brazil", NULL),
    ("Canada", NULL),
    ("China", NULL),
    ("France", NULL),
    ("Germany", NULL),
    ("India", NULL),
    ("Italy", NULL),
    ("Japan", NULL),
    ("United Kingdom", NULL),
    ("United States", NULL);