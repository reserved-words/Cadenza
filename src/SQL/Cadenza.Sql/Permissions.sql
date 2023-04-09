
IF NOT EXISTS (SELECT [name] FROM sys.database_principals WHERE [name] = '$(APIUser)')
BEGIN
	CREATE USER [$(APIUser)] FOR LOGIN [$(APIUser)]
END

GRANT EXECUTE ON SCHEMA::[Library] TO [$(APIUser)]
GRANT EXECUTE ON SCHEMA::[Queue] TO [$(APIUser)]