
public class Player 
{
    public string Email;
    public float Time;
    public int Kills;
    public int Deaths;

    public Player(string email, float time, int kills, int deaths)
    {
        this.Email = email;
        this.Time = time;
        this.Kills = kills;
        this.Deaths = deaths;
    }
}
