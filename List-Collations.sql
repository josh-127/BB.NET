
SELECT name AS [Name], COLLATIONPROPERTY(name, 'CodePage') AS [Code Page], description AS [Description]
    FROM sys.fn_helpcollations();