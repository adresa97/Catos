public class SendFoodEvent
{
    public SendFoodEvent() { }
}

public class SendCatEvent
{
    public int catIndex
    {
        get;
        private set;
    }

    public int hatIndex
    {
        get;
        private set;
    }

    public int shirtIndex
    {
        get;
        private set;
    }

    public SendCatEvent(int catIndex, int hatIndex, int shirtIndex)
    {
        this.catIndex = catIndex;
        this.hatIndex = hatIndex;
        this.shirtIndex = shirtIndex;
    }
}