# SymbolTracker
- Change time interval if required: SymbolReporterActor.cs 
- Default time interval: 4seconds
```csharp
    protected override void PreStart()
        {
            _getPriceScheduler = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(
                TimeSpan.FromSeconds(4),
                TimeSpan.FromSeconds(4),
                _lookupActor,
                new ApiDataRequest(),
                Self);
        }```
