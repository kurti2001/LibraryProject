namespace LibraryProject.DataAccess
{
	public static class Utils
	{
        internal static string ConnectionString { get; private set; }
		public static void Init(string connectionString)
		{
			ConnectionString = connectionString;
		}
	}
}
