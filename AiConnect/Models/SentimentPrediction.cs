using Microsoft.ML.Data;

namespace AiConnect.Models
{
    public class SentimentPrediction
    {
        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; } // Resultado da análise de sentimento (true ou false)

        public float Probability { get; set; } // Probabilidade da previsão
    }
}
