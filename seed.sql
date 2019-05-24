
USE `BBNet`;

CALL CreateCommunity('My Community', 'Description of a sample community.');
CALL CreateForum('Programming Help', 'Ask general programming questions here.', '', 1);

CALL GetCommunityListings();
CALL GetForumIndex(1);