namespace Blog.Constants
{
    public static class StringConstants
    {
        public const string SymmetricKey = "TESTTESTTESTTEST";
        public const string SymmetricSalt = "1234abcd5678efgh";

        public const string PacketEnding = "/rn/rn/rn$$";
        public const string UnrecognizedCommandAnswer = "QUE?";
        public const string LoginRequiredAnswer = "IDENTIFY_PLS";
        public const string LogoutPacketName = "THX_BYE";
        public const string ConnectionClosePacketName = "EOT";
        public const string GlobalPacketFormat = "{0}\t{1}\t{2}";

        // Packet defaults
        // Register
        public const string RegisterPacketName = "REGISTER";
        public const string RegisterPacketFormat = "{0}\t{1}";
        public const string RegisterPacketAnswerFormat = "{0}\t{1}";
        public const string RegisterPacketAnswerOK = "OK";
        public const string RegisterPacketAnswerInvalid = "INVALID";

        // Login
        public const string LoginPacketName = "LOGIN";
        public const string LoginPacketFormat = "{0}\t{1}\t{2}";
        public const string LoginPacketAnswerFormat = "{0}\t{1}\t{2}";
        public const string LoginPacketAnswerOK = "OK";
        public const string LoginPacketAnswerFailed = "FAILED";
        public const string LoginPacketAnswerFailedInvalid = "INVALID";
        public const string LoginPacketAnswerFailedLocked = "LOCKED";

        // Display blogs
        public const string DisplayBlogsPacketName = "DISPLAY_BLOGS";
        public const string DisplayBlogsPacketFormat = "{0}";
        public const string DisplayBlogsPacketAnswerFormat = "{0}\t{1}";

        // Display blog
        public const string DisplayBlogPacketName = "DISPLAY_BLOG";
        public const string DisplayBlogPacketFormat = "{0}\t{1}";
        public const string DisplayBlogPacketAnswerFormat = "{0}\t{1}";
        public const string DisplayBlogPacketAnswerFailed = "FAILED";

        // Add entry
        public const string AddEntryPacketName = "ADD_ENTRY";
        public const string AddEntryPacketFormat = "{0}\t{1}\t{2}";
        public const string AddEntryPacketAnswerFormat = "{0}\t{1}\t{2}";
        public const string AddEntryPacketAnswerOK = "OK";
        public const string AddEntryPacketAnswerInvalid = "INVALID";
        public const string AddEntryPacketAnswerInvalidTitle = "TITLE";
        public const string AddEntryPacketAnswerInvalidContent = "CONTENT";

        // Display entry
        public const string DisplayEntryPacketName = "DISPLAY_ENTRY";
        public const string DisplayEntryPacketFormat = "{0}\t{1}";
        public const string DisplayEntryPacketAnswerFormat = "{0}\t{1}\t{2}\t{3}";

        // Delete entry
        public const string DeleteEntryPacketName = "DELETE_ENTRY";
        public const string DeleteEntryPacketFormat = "{0}\t{1}";
        public const string DeleteEntryPacketAnswerFormat = "{0}\t{1}\t{2}";
        public const string DeleteEntryPacketAnswerOK = "OK";
        public const string DeleteEntryPacketAnswerFailed = "FAILED";
        public const string DeleteEntryPacketAnswerFailedNotExist = "NOTEXIST";
        public const string DeleteEntryPacketAnswerFailedNotOwner = "NOTOWNER";

        // Change blog name
        public const string ChangeBlogNamePacketName = "CHANGE_BLOG_NAME";
        public const string ChangeBlogNamePacketFormat = "{0}\t{1}\t{2}";
        public const string ChangeBlogNamePacketAnswerFormat = "{0}\t{1}";
        public const string ChangeBlogNamePacketAnswerOK = "OK";
        public const string ChangeBlogNamePacketAnswerFailed = "FAILED";
        public const string ChangeBlogNamePacketAnswerNotOwner = "NOTOWNER";

        // Ping
        public const string PingPacketName = "PING";
        public const string PingPacketFormat = "{0}";
        public const string PingPacketAnswerFormat = "{0}";
        public const string PingPacketAnswer = "PONG";
    }
}