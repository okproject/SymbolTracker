# SymbolTracker
- Change time interval if required: SymbolReporterActor.cs 
    protected override void PreStart()
        {
            _getPriceScheduler = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(
                TimeSpan.FromSeconds(4),
                TimeSpan.FromSeconds(4),
                _lookupActor,
                new ApiDataRequest(),
                Self);
            // base.PreStart();
        }    
