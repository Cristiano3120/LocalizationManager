# 📖 LocalizationManager 📖

The **LocalizationManager** was originally created to handle **resources and languages** in ***WPF*** applications in a clean and MVVM-friendly way.

This README will guide you **step by step** through the setup and usage of the library 🚀

---

## 📦 Installation

Open your project terminal and run:

~~~bash
dotnet add package Cacx.LocalizationManager --version latest
~~~

---

## 💻 Usage Guide

### ✅ Step 1 – XAML Setup

Go to your `Window.xaml` and declare the required namespaces:

~~~xaml
xmlns:designLoc="clr-namespace:Cacx.LocalizationManager.Core;assembly=Cacx.LocalizationManager"
xmlns:mvvm="clr-namespace:SampleProject.MVVM"
~~~

> ⚠️ The `mvvm` namespace depends on your project structure.  
> You **should** use MVVM for this library to work optimally.

---

### 🧩 DataContext Setup

Add a **design-time DataContext** so the designer can show preview values:

~~~xaml
<d:Window.DataContext>
    <designLoc:DesignTimeWindowContext/>
</d:Window.DataContext>
~~~

Then set the runtime DataContext (optional if done in code):

~~~xaml
<Window.DataContext>
    <mvvm:MainWindowMVVM/>
</Window.DataContext>
~~~

---

## 🧠 Step 2 – MVVM Setup

Inside your ViewModel, add and initialize the `LocalizationProvider`:

~~~csharp
public LocalizationProvider Loc { get; }

public MainWindowMVVM()
{
    // We will explain the constructor parameters in the next step
    Loc = new LocalizationProvider(
        resourceName: "SampleProject.Resources.MainWindow.MainWindow",
        cultureInfo: null
    );
}
~~~

---

## 📂 Step 3 – Resource Folder Structure

This tutorial assumes the following structure:

~~~
MyApp (Project)
 └─ Resources
     ├─ Login
     │   ├─ Login.resx
     │   └─ Login.de-DE.resx
     └─ CreateAccount
         ├─ CreateAccount.resx
         └─ CreateAccount.de-DE.resx
~~~

🧠 **Important concept**:  
Each subfolder represents a **context** (usually a Window or View).

---

### 📌 Resource Name Convention

~~~csharp
new LocalizationProvider(
    resourceName: "SampleProject.Resources.Login.Login",
    cultureInfo: null
);
~~~

**Grammar:**

~~~
{ProjectName}.{ResourcesFolder}.{ContextFolder}.{BaseResxName}
~~~

- `cultureInfo`
  - `null` → system default language 🌍
  - or a specific `CultureInfo` (e.g. `de-DE`)

---

## 🔗 Step 4 – XAML Binding

You can now bind localized strings in XAML:

~~~xaml
<TextBlock Text="{Binding Loc[WelcomeMessage]}" />
~~~

⚠️ **Important**  
XAML bindings only support **strings**.

If you need:
- Streams
- Images
- Other objects  

➡️ You must retrieve them **via code**.

---

## 📝 Step 5 – Creating the RESX Files

Example: **Login Context**

1. Go to `MyApp → Resources`
2. Create a folder called `Login`
3. Create:
   - `Login.resx` (base file)
   - `Login.de-DE.resx` (German)

Add the same keys to all files and translate their values 🌐

---

## 🎉 Setup Complete!

Your localization system is now fully working 🚀

---

## ✨ Additional Features

The `LocalizationProvider` offers several useful methods:

~~~csharp
void UpdateContext(string resourceName);
~~~
🔄 Changes the active context (resource file)

~~~csharp
void UpdateCulture(CultureInfo culture);
~~~
🌍 Switches the language (same context)

~~~csharp
CultureInfo GetCulture();
~~~
📌 Returns the current culture

~~~csharp
Stream GetStream(string key);
~~~
📂 Retrieves a stream from the resource file

~~~csharp
object GetObject(string key);
~~~
📦 Retrieves an object from the resource file

> ⚠️ Objects **cannot be added via the RESX GUI**.  
> They must be added via code using `ResourceWriter`.  
> A helper method for this will be added in a future release.

---
