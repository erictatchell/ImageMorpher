/*
 *  Interaction.cs used for constants
 */
namespace ImageMorpher
{
    // Constants used to detect user intention with lines
    public static class Intention
    {
        public const int MOVING = 501;
        public const int RESIZING_START = 502;
        public const int RESIZING_END = 503;
        public const int DELETING = 504;
        public const int CREATING_NEW_LINE = 500;
    }

    // Constants for working with imagebase form type
    public static class ImageBaseType
    {
        public const int SOURCE = 1000;
        public const int DESTINATION = 1001;
        public const int TRANSITION = 1002;
        public const string SOURCE_STR = "Source";
        public const string DESTINATION_STR = "Destination";
        public const string TRANSITION_STR = "Transition";
    }
}
