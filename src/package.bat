@ECHO OFF
.nuget\nuget pack Xander.UI.Web\Xander.UI.Web.nuspec -Prop Configuration=Release -Symbol -Verbosity detailed -OutputDirectory pkg
