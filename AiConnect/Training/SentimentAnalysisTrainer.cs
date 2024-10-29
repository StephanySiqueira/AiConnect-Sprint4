using AiConnect.Models; 
using Microsoft.ML;
using System.Collections.Generic;

namespace AiConnect.Training
{
    public class SentimentAnalysisTrainer
    {
        private readonly MLContext _mlContext;

        public SentimentAnalysisTrainer()
        {
            _mlContext = new MLContext();
        }

        public void TrainAndSaveModel(string modelPath)
        {
            // Chama o método estático de SentimentDataGenerator
            var sentimentData = SentimentDataGenerator.GetSentimentData();

            // Carregar os dados no formato adequado
            var data = _mlContext.Data.LoadFromEnumerable(sentimentData);

            // Pipeline de transformação
            var pipeline = _mlContext.Transforms.Text.FeaturizeText("Features", nameof(SentimentData.Text))
               .Append(_mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: nameof(SentimentData.Sentiment), maximumNumberOfIterations: 100));


            // Treinamento do modelo
            var model = pipeline.Fit(data);

            // Salvar o modelo
            _mlContext.Model.Save(model, data.Schema, modelPath);
        }
    }
}
