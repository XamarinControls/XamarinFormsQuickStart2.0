# Xamarin.Forms ReactiveUI Quickstart using .NetStandard 2.0
### Legacy PCL version found [here](https://github.com/helzgate/XamarinFormsQuickStart)

## Using:
* .NetStandard 2.0 library instead of PCL
* Akavache
* Autofac
* GoogleAnalytics (partially, see Notes)
* ReactiveUI

## Pages:
* About Page 
* Terms Page (to disable set IsTermsPageEnabled to false  in Target\Factories\DefaultsFactory.cs)
* Policy Page (to disable set IsTermsPageEnabled to false  in Target\Factories\DefaultsFactory.cs)
* Login Page (easily turn off in Target\Factories\DefaultsFactory.cs)
* Settings Page
* Home Page

## Features:
* SVG icons
* User defined font size
* Base level theming via Target\Factories\DefaultsFactory.cs
* Login button has a random chance of working, allowing you to setup failure logic even though no authentication system is being called yet.
* User settings are stored using Akavache (Sqlite)
* About, Policy, and Terms page allow you to use HTML
* Unit Tests (not much code coverage but getting there)

## Works Using:
* Android
* iOS
* UWP (You must be running on Windows 10 with Fall Creators Update)

## My Environment
* Windows 10 Professional with Fall Creator's update
* Visual Studio Enterprise v15.5.2

## Notes
* I'm not using ReactiveUI routing because my understanding is that it doesn't work with MasterDetailPage according to [this](https://stackoverflow.com/questions/28624011/xamarin-form-reactive-ui-masterdetailpage) Stack Overflow.  I tried anyway but couldn't get it working.
* I had to disable Google Analytics in shared code because the Xamarin Forms plugin for Google Analytics doesn't work in .NetStandard yet.  I opened a case with the developer and he said he's working on it.  Google Analytics should still work within the native projects.  I'll probably write a dependency service in the mean time to handle this for me. 