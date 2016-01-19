using Foundation;
using MonoTouch.Dialog;
using System;
using System.IO;
using UIKit;


namespace MonoTouchExample
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        UINavigationController navigation;
        UIWindow window;

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            JsonElement sampleJson;
            var Last = new DateTime (2010, 10, 7);
            Console.WriteLine (Last);
            
            var p = Path.GetFullPath ("background.png");

            var menu = new RootElement ("Demos"){
                new Section ("Json") {
                    (sampleJson = JsonElement.FromFile ("sample.json")),
                    // Notice what happens when I close the parenthesis at the end, in the next line:
                    new JsonElement ("Load from URL", "file://" + Path.GetFullPath ("sample.json"))
                }
            };
            
            //
            // Lookup elements by ID:
            //
            var jsonSection = sampleJson ["section-1"] as Section;
            Console.WriteLine ("The section has {0} elements", jsonSection.Count);
            var booleanElement = sampleJson ["first-boolean"] as BooleanElement;
            Console.WriteLine ("The state of the first-boolean value is {0}", booleanElement.Value);
            
            //
            // Create our UI and add it to the current top-level navigation controller
            // this will allow us to have nice navigation animations.
            //
            var dv = new DialogViewController (menu) {
                Autorotate = true
            };
            navigation = new UINavigationController ();
            navigation.PushViewController (dv, true);				
            
            // On iOS5 we use the new window.RootViewController, on older versions, we add the sub view
            window = new UIWindow (UIScreen.MainScreen.Bounds);
            window.MakeKeyAndVisible ();
            if (UIDevice.CurrentDevice.CheckSystemVersion (5, 0))
                window.RootViewController = navigation;	
            else
                window.AddSubview (navigation.View);
            
            return true;
        }

        static void JsonCallback (object data)
        {
            Console.WriteLine ("Invoked");
        }        

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background execution this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transition from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
        }

        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }
    }
}


