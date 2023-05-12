namespace CED.Contracts.Charts;

public record DonutData(int value, string name);
public record DonutChartData(List<DonutData> DonutDatas);
