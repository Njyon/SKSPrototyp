# Asset Reference Viewer

This is forked version of Project-Curator. I heavily modified this tool by adding graph view moving UI to Unity UI Elements and more.

A convenient tool to cleanup and maintain your Unity projects !    
**Asset Reference Viewer** is an Unity Editor window that, based on the currently selected Asset, displays the following information :

- Each asset it depends on (called **dependencies**)

- Each asset that depends on it (called **referencers**)  
  *Very useful to know if an Asset is can be safely deleted or not*

- Whether the asset is included in the build or not, depending on the nature of the Asset and its referencers (checked recursively)  
  *Check statuses section for more information*

![Screenshot](https://raw.githubusercontent.com/Dasparion/AssetReferenceViewer/master/Demo/preview.gif)

## How to use ?
- Install package
  - Using Git : In Unity, click **Window > Package Manager > + > Add package from git URL...** and add `https://github.com/Dasparion/AssetReferenceViewer.git`
- Select an asset to visualize dependencies and references.

## Statuses

Statuses can be :

- Unknown
- Not Included in Build
  - **Not Includable**  
    *This asset can't be in the build.*  
    *Example : Editor scripts*
  - **Not Included**  
    *This asset is not included in the build.  
    Example : None of its referencers are included in the build*
- Included in Build
  - **Scene In Build**  
    *The asset is a scene and is set to build*
  - **Runtime Script**  
    *The asset is a runtime script*
  - **Resource Asset**  
    *The asset is in a Resources folder and will end in the final build  
    It does not mean that the asset is actually useful. Check referencers manually and Resources.Load calls to find out*
  - **Referenced**  
    *The asset is referenced by at least one other asset that is included in the build  
    Example : A prefab that is in a built Scene*  

> The overlay icon in the project folder can be disabled. To do so, right click on the Project Curator window tab, and click "Project Overlay"
