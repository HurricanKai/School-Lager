using System.Text.Json;
using Microsoft.JSInterop;

namespace Lager.Shared;

public sealed class LocalForage
{
    private readonly IJSInProcessRuntime _runtime;

    public LocalForage(IJSInProcessRuntime runtime)
    {
        _runtime = runtime;
    }
    
    public async ValueTask<LocalForageStore> CreateStore(string name, string description)
    {
        var obj = await _runtime.InvokeAsync<IJSInProcessObjectReference>("localforage.createInstance",
            new { name = "LAGER_DB", storeName = name, description = description });
        await _runtime.InvokeVoidAsync("console.log", "Created Instance: ", obj);
        return new(_runtime, obj);
    }
}

public sealed class LocalForageStore : IDisposable
{
    private readonly IJSInProcessRuntime _runtime;
    private readonly IJSInProcessObjectReference _instance;

    public LocalForageStore(IJSInProcessRuntime runtime, IJSInProcessObjectReference instance)
    {
        _runtime = runtime;
        _instance = instance;
    }

    public async ValueTask<T?> GetItem<T>(string key)
    {
        await _runtime.InvokeVoidAsync("console.log", $"Getting {key} from ", _instance);
        return JsonSerializer.Deserialize<T>(await _instance.InvokeAsync<string>("getItem", key));
    }
    
    public async ValueTask SetItem<T>(string key, T value)
    {
        await _runtime.InvokeVoidAsync("console.log", $"Setting {key} in ", _instance);
        await _instance.InvokeVoidAsync("setItem", key, JsonSerializer.Serialize(value));
    }
    
    public async ValueTask RemoveItem(string key)
    {
        await _runtime.InvokeVoidAsync("console.log", $"Removing {key} from ", _instance);
        await _instance.InvokeVoidAsync("removeItem", key);
    }

    public async ValueTask Clear()
    {
        await _runtime.InvokeVoidAsync("console.log", $"CLEARING", _instance);
        await _instance.InvokeVoidAsync("clear");
    }
    
    public async ValueTask<int> Length()
    {
        await _runtime.InvokeVoidAsync("console.log", $"Getting LENGTH from ", _instance);
        return await _instance.InvokeAsync<int>("length");
    }
    
    public async ValueTask<string[]> Keys()
    {
        await _runtime.InvokeVoidAsync("console.log", $"Getting keys from ", _instance);
        return await _instance.InvokeAsync<string[]>("keys");
    }

    public void Dispose()
    {
        _instance.Dispose();
    }
}