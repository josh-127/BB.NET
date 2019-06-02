USE `PicoBoards`;

CREATE OR REPLACE VIEW `vw_UserListing` AS
    SELECT      `UserName`,
                g.`Name` AS `GroupName`,
                `Created`,
                `LastActive`
    FROM        `User` u
    INNER JOIN  `Group` g
    ON          u.`GroupId` = g.`GroupId`;

CREATE OR REPLACE VIEW `vw_UserProfileDetails` AS
    SELECT      `UserName`,
                g.`Name` AS `GroupName`,
                `Created`,
                `LastActive`,
                `Birthday`,
                `Location`,
                `Signature`
    FROM        `User` u
    INNER JOIN  `Group` g
    ON          u.`GroupId` = g.`GroupId`;