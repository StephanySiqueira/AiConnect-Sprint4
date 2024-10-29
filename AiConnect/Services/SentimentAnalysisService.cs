using AiConnect.Models;
using AiConnect.Training;
using Microsoft.ML;

namespace AiConnect.Services
{
    public class SentimentAnalysisService
    {
        private readonly MLContext _mlContext;
        private ITransformer _model;

        public SentimentAnalysisService()
        {
            _mlContext = new MLContext();
            // Carregar o modelo treinado
            _model = LoadModel(@"C:\Users\tesiq\Downloads\AiConnect-Sprint4\AiConnect-Sprint4\AiConnect\TrainedModels\sentimentModel.zip");
        }

        public SentimentPrediction Predict(string text)
        {
            var input = new SentimentData { Text = text };
            var inputData = _mlContext.Data.LoadFromEnumerable(new[] { input });
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(_model);
            var result = predictionEngine.Predict(input);
            return result;
        }

        private ITransformer LoadModel(string modelPath)
        {
            return _mlContext.Model.Load(modelPath, out var _);
        }
    }
}