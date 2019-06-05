USE `PicoBoards`;

CREATE OR REPLACE VIEW `vw_UserListing` AS
    SELECT      `UserId`,
                `UserName`,
                g.`Name` AS `GroupName`,
                `Created`,
                `LastActive`
    FROM        `User` u
    INNER JOIN  `Group` g
    ON          u.`GroupId` = g.`GroupId`;

CREATE OR REPLACE VIEW `vw_UserProfileDetails` AS
    SELECT      `UserId`,
                `EmailAddress`,
                `UserName`,
                g.`Name` AS `GroupName`,
                `Created`,
                `LastActive`,
                `Birthday`,
                `Location`,
                `Signature`
    FROM        `User` u
    INNER JOIN  `Group` g
    ON          u.`GroupId` = g.`GroupId`;

CREATE OR REPLACE VIEW `vw_PostListing` AS
    SELECT      `Post`.`PostId`,
                `Post`.`TopicId`,
                `Post`.`Name`,
                `Post`.`Body`,
                `Post`.`Created`,
                `Post`.`Modified`,
                `Post`.`FormattingEnabled`,
                `Post`.`SmiliesEnabled`,
                `Post`.`ParseUrls`,
                `Post`.`AttachSignature`,
                `User`.`UserId`,
                `User`.`UserName`,
                `User`.`Signature`
    FROM        `Post`
    INNER JOIN  `User`
    ON          `Post`.`UserId` = `User`.`UserId`;