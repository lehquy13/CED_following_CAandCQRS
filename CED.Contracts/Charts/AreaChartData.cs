namespace CED.Contracts.Charts;

public record AreaData( string name,List<float> data);
public record AreaChartData(AreaData totalRevuenues,AreaData incoming,AreaData cenceleds,List<string> dates);
