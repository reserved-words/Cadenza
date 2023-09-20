namespace Cadenza.State.Reducers;

public static class ConnectorReducers
{
    [ReducerMethod]
    public static ConnectorState ReduceConnectorStatusUpdateRequest(ConnectorState state, ConnectorStatusUpdateRequest action) 
    {
        if (!state.Connectors.ContainsKey(action.Connector))
        {
            state.Connectors.Add(action.Connector, ConnectorStatus.Loading);
        }

        state.Connectors[action.Connector] = action.Status;
        return state;
    }
}
