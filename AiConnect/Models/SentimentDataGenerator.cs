namespace AiConnect.Models
{
    public class SentimentDataGenerator
    {
        public static IEnumerable<SentimentData> GetSentimentData()
        {
            return new List<SentimentData>
            {
                // Sentimentos positivos
                new SentimentData { Text = "O AiConnect facilitou muito a gestão dos meus clientes!", Sentiment = true },
                new SentimentData { Text = "A interface é intuitiva e fácil de usar.", Sentiment = true },
                new SentimentData { Text = "Excelente suporte! Sempre me ajudaram quando precisei.", Sentiment = true },
                new SentimentData { Text = "Os relatórios gerados são muito úteis para minha equipe.", Sentiment = true },
                new SentimentData { Text = "Estou impressionado com a rapidez do sistema.", Sentiment = true },
                new SentimentData { Text = "Recomendo o AiConnect para qualquer empresa que queira melhorar suas vendas!", Sentiment = true },
                new SentimentData { Text = "O sistema me ajudou a organizar minha base de clientes de forma eficaz.", Sentiment = true },
                new SentimentData { Text = "A integração com outras ferramentas é excelente!", Sentiment = true },
                new SentimentData { Text = "Um ótimo investimento para aumentar a produtividade!", Sentiment = true },
                new SentimentData { Text = "Estou muito satisfeito com os resultados que obtivemos até agora.", Sentiment = true },
                new SentimentData { Text = "Eu amei.", Sentiment = true },

                // Sentimentos negativos
                new SentimentData { Text = "Tive dificuldades para entender algumas funcionalidades do sistema.", Sentiment = false },
                new SentimentData { Text = "O suporte demorou para responder minha solicitação.", Sentiment = false },
                new SentimentData { Text = "Algumas funcionalidades não funcionam como esperado.", Sentiment = false },
                new SentimentData { Text = "O sistema é um pouco lento em horários de pico.", Sentiment = false },
                new SentimentData { Text = "A integração com meu e-mail não está funcionando corretamente.", Sentiment = false },
                new SentimentData { Text = "Não consegui gerar os relatórios que precisava.", Sentiment = false },
                new SentimentData { Text = "Sinto que falta treinamento para minha equipe.", Sentiment = false },
                new SentimentData { Text = "Não é tão intuitivo quanto esperava.", Sentiment = false },
                new SentimentData { Text = "Não achei a ferramenta tão fácil de usar quanto anunciado.", Sentiment = false },
                new SentimentData { Text = "Acho que o sistema poderia ter mais recursos de personalização.", Sentiment = false },
                new SentimentData { Text = "Eu odiei.", Sentiment = false },
            };
        }
    }
}
