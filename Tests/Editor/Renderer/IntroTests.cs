using System.Collections;
using NUnit.Framework;
using ReactUnity.Scripting;
using ReactUnity.UIToolkit;
using UnityEngine;
using UnityEngine.UIElements;

namespace ReactUnity.Tests.Editor.Renderer
{
    public class IntroTests : EditorTestBase
    {
        public IntroTests(JavascriptEngineType engineType) : base(engineType) { }

        [EditorInjectableTest(Script = @"
            const App = () => (
                <view>
                    Hello world
                    <view>Hello again</view>
                    <view>
                        Somehow
                        <view> just hello</view>
                    </view>
                </view>
            );
        ")]
        public IEnumerator TextContent_IsCorrect()
        {
            yield return null;

            Assert.AreEqual("Hello worldHello againSomehow just hello", Host.TextContent);
        }

        [EditorInjectableTest]
        public IEnumerator HostNameCanBeChanged()
        {
            yield return null;
            Assert.AreEqual(null, Host.Name);

            Host.Name = "hey";
            Assert.AreEqual("hey", Host.Name);

            Host.Name = null;
            Assert.AreEqual(null, Host.Name);
        }

        [EditorInjectableTest(Script = @"
            const App = () => (
                <view style={{ color: Interop.UnityEngine.Color.red }}>
                    Hello world
                </view>
            );
        ")]
        public IEnumerator InteropWorks()
        {
            yield return null;

            var cmp = Q("view") as UIToolkitComponent<VisualElement>;
            var rt = cmp.Element;

            Assert.AreEqual(Color.red, rt.style.color.value);
        }
    }
}
