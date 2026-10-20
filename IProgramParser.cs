interface IProgramParser
{
    bool canParse(string extension);
    Program Parse(string content);
}