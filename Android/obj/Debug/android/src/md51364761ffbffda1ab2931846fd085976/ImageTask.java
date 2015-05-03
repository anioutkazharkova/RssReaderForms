package md51364761ffbffda1ab2931846fd085976;


public class ImageTask
	extends android.os.AsyncTask
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_doInBackground:([Ljava/lang/Object;)Ljava/lang/Object;:GetDoInBackground_arrayLjava_lang_Object_Handler\n" +
			"";
		mono.android.Runtime.register ("RssReaderForms.Android.ImageTask, RssReaderForms.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ImageTask.class, __md_methods);
	}


	public ImageTask () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ImageTask.class)
			mono.android.TypeManager.Activate ("RssReaderForms.Android.ImageTask, RssReaderForms.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public ImageTask (java.lang.String p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == ImageTask.class)
			mono.android.TypeManager.Activate ("RssReaderForms.Android.ImageTask, RssReaderForms.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0 });
	}


	public java.lang.Object doInBackground (java.lang.Object[] p0)
	{
		return n_doInBackground (p0);
	}

	private native java.lang.Object n_doInBackground (java.lang.Object[] p0);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
