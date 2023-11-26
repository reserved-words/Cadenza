
IF '$(APIUser)' != 'N/A'
BEGIN

	IF NOT EXISTS (SELECT [name] FROM sys.database_principals WHERE [name] = '$(APIUser)')
	BEGIN
		CREATE USER [$(APIUser)] FOR LOGIN [$(APIUser)]
	END
	
	GRANT EXECUTE ON SCHEMA::[Admin] TO [$(APIUser)]
	GRANT EXECUTE ON SCHEMA::[History] TO [$(APIUser)]
	GRANT EXECUTE ON SCHEMA::[Images] TO [$(APIUser)]
	GRANT EXECUTE ON SCHEMA::[Library] TO [$(APIUser)]
	GRANT EXECUTE ON SCHEMA::[Play] TO [$(APIUser)]
	GRANT EXECUTE ON SCHEMA::[Queue] TO [$(APIUser)]
	GRANT EXECUTE ON SCHEMA::[Search] TO [$(APIUser)]
	GRANT EXECUTE ON SCHEMA::[Update] TO [$(APIUser)]

END

IF '$(ServiceUser)' != 'N/A'
BEGIN

	IF NOT EXISTS (SELECT [name] FROM sys.database_principals WHERE [name] = '$(ServiceUser)')
	BEGIN
		CREATE USER [$(ServiceUser)] FOR LOGIN [$(ServiceUser)]
	END
	
	GRANT EXECUTE ON SCHEMA::[History] TO [$(ServiceUser)]
	GRANT EXECUTE ON SCHEMA::[Library] TO [$(ServiceUser)]
	GRANT EXECUTE ON SCHEMA::[Queue] TO [$(ServiceUser)]
	GRANT EXECUTE ON SCHEMA::[Update] TO [$(ServiceUser)]

END