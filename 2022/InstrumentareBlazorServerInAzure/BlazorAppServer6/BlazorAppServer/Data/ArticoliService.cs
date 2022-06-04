namespace BlazorAppServer.Data
{
    public class ArticoliService
    {
        

        public Task<Articolo[]> GetArticoliAsync(DateTime startDate)
        {



            Random rnd = new Random();

            


            return Task.FromResult( Enumerable.Range(1, 200).Select(index => new Articolo
            {
                CodiceArticolo = $"CodArt{index.ToString()}",
                Descrizione = $"Descrizione{index.ToString()}",
                URLPhoto=$"./img/monkey{rnd.Next(2)}.jpg"

            })
           .ToArray());
        }
    }
}