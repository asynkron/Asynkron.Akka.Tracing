using Akka.Actor;
using Akka.Actor.Internal;
using Akka.Event;
using Asynkron.Akka.Decorators;

namespace Asynkron.Akka.OpenTelemetry;

public sealed class OpenTelemetryActorRefProvider : DecoratorActorRefProvider
{
    public OpenTelemetryActorRefProvider(string systemName, Settings settings, EventStream eventStream)
    {
        var inner = new LocalActorRefProvider(systemName, settings, eventStream);
        Init(inner);
    }

    public override IInternalActorRef ActorOf(ActorSystemImpl system, Props props, IInternalActorRef supervisor,
        ActorPath path,
        bool systemService, Deploy deploy, bool lookupDeploy, bool async)
    {
        var reff = base.ActorOf(system, props, supervisor, path, systemService, deploy, lookupDeploy, async);
        return new OpenTelemetryActorRef(reff);
    }
}