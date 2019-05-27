
CREATE SCHEMA IF NOT EXISTS `bbnet`
    DEFAULT CHARACTER SET utf8mb4
    DEFAULT COLLATE utf8mb4_0900_ai_ci;

USE `bbnet`;

CREATE TABLE IF NOT EXISTS `category` (
    `category_id`   INT         NOT NULL    AUTO_INCREMENT,
    `name`          VARCHAR(45),
    PRIMARY KEY (`category_id`)
);

CREATE TABLE IF NOT EXISTS `forum` (
    `forum_id`      INT         NOT NULL    AUTO_INCREMENT,
    `category_id`   INT         NOT NULL,
    `name`          VARCHAR(45) NOT NULL,
    `description`   TINYTEXT,
    `image_url`     TINYTEXT,
    `created`       TIMESTAMP   NOT NULL    DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (`forum_id`),
    INDEX `idx_forum_category_id` (`category_id`),
    CONSTRAINT `fk_forum_category_id`
        FOREIGN KEY (`category_id`)
        REFERENCES `category` (`category_id`)
);

CREATE TABLE IF NOT EXISTS `topic_banner` (
    `topic_banner_id`   INT         NOT NULL    AUTO_INCREMENT,
    `image_url`         VARCHAR(45) NOT NULL,
    PRIMARY KEY (`topic_banner_id`)
);

CREATE TABLE IF NOT EXISTS `topic` (
    `topic_id`          INT         NOT NULL    AUTO_INCREMENT,
    `forum_id`          INT         NOT NULL,
    `name`              VARCHAR(45) NOT NULL,
    `description`       VARCHAR(45),
    `topic_banner_id`   INT,
    `created`           TIMESTAMP   NOT NULL    DEFAULT CURRENT_TIMESTAMP,
    `is_locked`         TINYINT     NOT NULL,
    `is_sticky`         TINYINT     NOT NULL,
    PRIMARY KEY (`topic_id`),
    INDEX `idx_topic_forum_id` (`forum_id`),
    INDEX `idx_topic_topic_banner_id` (`topic_banner_id`),
    CONSTRAINT `fk_topic_forum_id`
        FOREIGN KEY (`forum_id`)
        REFERENCES `forum` (`forum_id`),
    CONSTRAINT `fk_topic_topic_banner_id`
        FOREIGN KEY (`topic_banner_id`)
        REFERENCES `topic_banner` (`topic_banner_id`)
);

CREATE TABLE IF NOT EXISTS `group` (
    `group_id`      INT         NOT NULL    AUTO_INCREMENT,
    `name`          VARCHAR(45) NOT NULL,
    `description`   VARCHAR(45),
    PRIMARY KEY (`group_id`)
);

CREATE TABLE IF NOT EXISTS `gender` (
    `gender_id`     INT             NOT NULL    AUTO_INCREMENT,
    `name`          VARCHAR(45)     NOT NULL,
    `image_url`     VARCHAR(45),
    PRIMARY KEY (`gender_id`)
);

CREATE TABLE IF NOT EXISTS `country` (
    `country_id`    INT             NOT NULL    AUTO_INCREMENT,
    `name`          VARCHAR(45)     NOT NULL,
    `image_url`     VARCHAR(45),
    PRIMARY KEY (`country_id`)
);

CREATE TABLE IF NOT EXISTS `profile_image` (
    `profile_image_id`  INT         NOT NULL    AUTO_INCREMENT,
    `file_name`         VARCHAR(45) NOT NULL,
    `image_url`         VARCHAR(45) NOT NULL,
    PRIMARY KEY (`profile_image_id`)
);

CREATE TABLE IF NOT EXISTS `user` (
    `user_id`           INT         NOT NULL    AUTO_INCREMENT,
    `group_id`          INT         NOT NULL,
    `email`             VARCHAR(45) NOT NULL,
    `username`          VARCHAR(45) NOT NULL,
    `gender_id`         INT         NOT NULL,
    `country_id`        INT         NOT NULL,
    `profile_image_id`  INT         NOT NULL,
    `birthday`          DATE,
    `interests`         MEDIUMTEXT,
    `occupation`        MEDIUMTEXT,
    `location`          VARCHAR(45),
    `signature`         MEDIUMTEXT,
    PRIMARY KEY (`user_id`),
    INDEX `idx_user_group_id` (`group_id`),
    INDEX `idx_user_gender_id` (`gender_id`),
    INDEX `idx_user_country_id` (`country_id`),
    INDEX `idx_user_profile_image_id` (`profile_image_id`),
    CONSTRAINT `fk_user_group_id`
        FOREIGN KEY (`group_id`)
        REFERENCES `group` (`group_id`),
    CONSTRAINT `fk_user_gender_id`
        FOREIGN KEY (`gender_id`)
        REFERENCES `gender` (`gender_id`),
    CONSTRAINT `fk_user_country_id`
        FOREIGN KEY (`country_id`)
        REFERENCES `country` (`country_id`),
    CONSTRAINT `fk_user_profile_image_id`
        FOREIGN KEY (`profile_image_id`)
        REFERENCES `profile_image` (`profile_image_id`)
);

CREATE TABLE IF NOT EXISTS `post` (
    `post_id`               INT         NOT NULL    AUTO_INCREMENT,
    `topic_id`              INT         NOT NULL,
    `user_id`               INT         NOT NULL,
    `name`                  VARCHAR(45) NOT NULL,
    `body`                  MEDIUMTEXT  NOT NULL,
    `created`               TIMESTAMP   NOT NULL    DEFAULT CURRENT_TIMESTAMP,
    `modified`              TIMESTAMP   NOT NULL    DEFAULT CURRENT_TIMESTAMP,
    `formatting_enabled`    TINYINT     NOT NULL    DEFAULT 1,
    `smilies_enabled`       TINYINT     NOT NULL    DEFAULT 1,
    `parse_urls`            TINYINT     NOT NULL    DEFAULT 1,
    `attach_signature`      TINYINT     NOT NULL    DEFAULT 1,
    PRIMARY KEY (`post_id`),
    INDEX `idx_post_topic_id` (`topic_id`),
    INDEX `idx_post_user_id` (`user_id`),
    CONSTRAINT `fk_post_topic_id`
        FOREIGN KEY (`topic_id`)
        REFERENCES `topic` (`topic_id`),
    CONSTRAINT `fk_post_user_id`
        FOREIGN KEY (`user_id`)
        REFERENCES `user` (`user_id`)
);

CREATE TABLE IF NOT EXISTS `attachment` (
    `attachment_id` INT         NOT NULL    AUTO_INCREMENT,
    `post_id`       INT         NOT NULL,
    `file_name`     VARCHAR(45) NOT NULL,
    `contents`      MEDIUMBLOB  NOT NULL,
    PRIMARY KEY (`attachment_id`),
    INDEX `idx_attachment_post_id` (`post_id`),
    CONSTRAINT `fk_attachment_post_id`
        FOREIGN KEY (`post_id`)
        REFERENCES `post` (`post_id`)
);

CREATE TABLE IF NOT EXISTS `capability` (
    `capability_id` VARCHAR(19) NOT NULL,
    `description`   MEDIUMTEXT,
    PRIMARY KEY (`capability_id`)
);

CREATE TABLE IF NOT EXISTS `group_permission` (
    `group_id`      INT         NOT NULL,
    `capability_id` VARCHAR(19) NOT NULL,
    PRIMARY KEY (`group_id`, `capability_id`),
    INDEX `idx_group_permission_capability_id` (`capability_id`),
    CONSTRAINT `fk_group_permission_group_id`
        FOREIGN KEY (`group_id`)
        REFERENCES `group` (`group_id`),
    CONSTRAINT `fk_group_permission_capability_id`
        FOREIGN KEY (`capability_id`)
        REFERENCES `capability` (`capability_id`)
);

CREATE TABLE IF NOT EXISTS `user_permission` (
    `user_id`       INT         NOT NULL,
    `capability_id` VARCHAR(19) NOT NULL,
    PRIMARY KEY (`user_id`, `capability_id`),
    INDEX `idx_user_permission_capability_id` (`capability_id`),
    CONSTRAINT `fk_user_permission_user_id`
        FOREIGN KEY (`user_id`)
        REFERENCES `user` (`user_id`),
    CONSTRAINT `fk_user_permission_capability_id`
        FOREIGN KEY (`capability_id`)
        REFERENCES `capability` (`capability_id`)
);

CREATE TABLE IF NOT EXISTS `poll` (
    `poll_id`       INT         NOT NULL    AUTO_INCREMENT,
    `topic_id`      INT         NOT NULL,
    `prompt`        VARCHAR(45) NOT NULL,
    `expiration`    DATETIME,
    `max_choices`   INT         NOT NULL,
    PRIMARY KEY (`poll_id`),
    INDEX `idx_poll_topic_id` (`topic_id`),
    CONSTRAINT `fk_poll_topic_id`
        FOREIGN KEY (`topic_id`)
        REFERENCES `topic` (`topic_id`)
);

CREATE TABLE IF NOT EXISTS `poll_choice` (
    `poll_choice_id`    INT         NOT NULL    AUTO_INCREMENT,
    `poll_id`           INT         NOT NULL,
    `text`              VARCHAR(45) NOT NULL,
    PRIMARY KEY (`poll_choice_id`),
    INDEX `idx_poll_choice_poll_id` (`poll_id`),
    CONSTRAINT `fk_poll_choice_poll_id`
        FOREIGN KEY (`poll_id`)
        REFERENCES `poll` (`poll_id`)
);

CREATE TABLE IF NOT EXISTS `poll_vote` (
    `poll_choice_id`    INT NOT NULL,
    `user_id`           INT NOT NULL,
    PRIMARY KEY (`poll_choice_id`, `user_id`),
    CONSTRAINT `fk_poll_vote_poll_choice_id`
        FOREIGN KEY (`poll_choice_id`)
        REFERENCES `poll_choice` (`poll_choice_id`),
    CONSTRAINT `fk_poll_vote_user_id`
        FOREIGN KEY (`user_id`)
        REFERENCES `user` (`user_id`)
);

CREATE TABLE IF NOT EXISTS `forum_capability` (
    `forum_capability_id`   VARCHAR(19) NOT NULL,
    `description`           TEXT,
    PRIMARY KEY (`forum_capability_id`)
);

CREATE TABLE IF NOT EXISTS `forum_group_permission` (
    `forum_id`              INT         NOT NULL,
    `group_id`              INT         NOT NULL,
    `forum_capability_id`   VARCHAR(19) NOT NULL,
    PRIMARY KEY (`forum_id`, `group_id`, `forum_capability_id`),
    INDEX `idx_forum_group_permission_group_id` (`group_id`),
    INDEX `idx_forum_group_permission_forum_capability_id` (`forum_capability_id`),
    CONSTRAINT `fk_forum_group_permission_forum_id`
        FOREIGN KEY (`forum_id`)
        REFERENCES `forum` (`forum_id`),
    CONSTRAINT `fk_forum_group_permission_group_id`
        FOREIGN KEY (`group_id`)
        REFERENCES `group` (`group_id`),
    CONSTRAINT `fk_forum_group_permission_forum_capability_id`
        FOREIGN KEY (`forum_capability_id`)
        REFERENCES `forum_capability` (`forum_capability_id`)
);

CREATE TABLE IF NOT EXISTS `default_forum_group_permission` (
    `group_id`              INT         NOT NULL,
    `forum_capability_id`   VARCHAR(19) NOT NULL,
    PRIMARY KEY (`group_id`, `forum_capability_id`),
    CONSTRAINT `fk_default_forum_group_permission_group_id`
        FOREIGN KEY (`group_id`)
        REFERENCES `group` (`group_id`),
    CONSTRAINT `fk_default_forum_group_permission_forum_capability_id`
        FOREIGN KEY (`forum_capability_id`)
        REFERENCES `forum_capability` (`forum_capability_id`)
);

CREATE TABLE IF NOT EXISTS `forum_user_permission` (
    `forum_id`              INT         NOT NULL,
    `user_id`               INT         NOT NULL,
    `forum_capability_id`   VARCHAR(19) NOT NULL,
    PRIMARY KEY (`forum_id`, `user_id`, `forum_capability_id`),
    INDEX `idx_forum_user_permission_user_id` (`user_id`),
    INDEX `idx_forum_user_permission_forum_capability` (`forum_capability_id`),
    CONSTRAINT `fk_forum_user_permission_forum_id`
        FOREIGN KEY (`forum_id`)
        REFERENCES `forum` (`forum_id`),
    CONSTRAINT `fk_forum_user_permission_user_id`
        FOREIGN KEY (`user_id`)
        REFERENCES `user` (`user_id`),
    CONSTRAINT `fk_forum_user_permission_forum_capability_id`
        FOREIGN KEY (`forum_capability_id`)
        REFERENCES `forum_capability` (`forum_capability_id`)
);

CREATE TABLE IF NOT EXISTS `default_forum_user_permission` (
    `user_id`               INT         NOT NULL,
    `forum_capability_id`   VARCHAR(19) NOT NULL,
    PRIMARY KEY (`user_id`, `forum_capability_id`),
    CONSTRAINT `fk_default_forum_user_permission_user_id`
        FOREIGN KEY (`user_id`)
        REFERENCES `user` (`user_id`),
    CONSTRAINT `fk_default_forum_user_permission_forum_capability_id`
        FOREIGN KEY (`forum_capability_id`)
        REFERENCES `forum_capability` (`forum_capability_id`)
);

CREATE TABLE IF NOT EXISTS `post_like` (
    `post_id`   INT NOT NULL,
    `user_id`   INT NOT NULL,
    PRIMARY KEY (`post_id`, `user_id`),
    INDEX `idx_post_like_user_id` (`user_id`),
    CONSTRAINT `fk_post_like_post_id`
        FOREIGN KEY (`post_id`)
        REFERENCES `post` (`post_id`),
    CONSTRAINT `fk_post_like_user_id`
        FOREIGN KEY (`user_id`)
        REFERENCES `user` (`user_id`)
);

CREATE TABLE IF NOT EXISTS `unread_topic` (
    `topic_id`  INT NOT NULL,
    `user_id`   INT NOT NULL,
    PRIMARY KEY (`topic_id`, `user_id`),
    INDEX `idx_unread_topic_user_id` (`user_id`),
    CONSTRAINT `fk_unread_topic_topic_id`
        FOREIGN KEY (`topic_id`)
        REFERENCES `topic` (`topic_id`),
    CONSTRAINT `fk_unread_topic_user_id`
        FOREIGN KEY (`user_id`)
        REFERENCES `user` (`user_id`)
);

INSERT INTO `group`
    (`name`, `description`)
    VALUES
    ("Administrator", NULL),
    ("Global Moderator", NULL),
    ("Member", NULL);

INSERT INTO `gender`
    (`name`, `image_url`)
    VALUES
    ("Male", NULL),
    ("Female", NULL),
    ("Other", NULL),
    ("Prefer not to say", NULL);

INSERT INTO `country`
    (`name`, `image_url`)
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