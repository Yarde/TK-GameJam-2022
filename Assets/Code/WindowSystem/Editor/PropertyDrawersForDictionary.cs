using UnityEditor;
using Yarde.Utils.Editor;
using Yarde.WindowSystem.BlendProvider;
using Yarde.WindowSystem.WindowProvider;

namespace Yarde.WindowSystem.Editor
{
    [CustomPropertyDrawer(typeof(BlendProviderToType))] 
    public class BlendProviderToTypePropertyDrawer : SerializableDictionaryPropertyDrawer {}
    
    [CustomPropertyDrawer(typeof(WindowTypeToPrefab))] 
    public class WindowConfigPropertyDrawer : SerializableDictionaryPropertyDrawer {}
}
