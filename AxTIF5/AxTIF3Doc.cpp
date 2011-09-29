// AxTIF3Doc.cpp : CAxTIF3Doc クラスの実装
//

#include "stdafx.h"
#include "AxTIF3.h"

#include "AxTIF3Doc.h"
#include "SrvrItem.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CAxTIF3Doc

IMPLEMENT_DYNCREATE(CAxTIF3Doc, CDocument)

BEGIN_MESSAGE_MAP(CAxTIF3Doc, CDocument)
END_MESSAGE_MAP()


// CAxTIF3Doc コンストラクション/デストラクション

CAxTIF3Doc::CAxTIF3Doc()
{
}

CAxTIF3Doc::~CAxTIF3Doc()
{
}

BOOL CAxTIF3Doc::OnNewDocument()
{
	if (!CDocument::OnNewDocument())
		return FALSE;

	m_tifs.RemoveAll();

	return TRUE;
}

class CMFCFile : public CxFile {
public:
	CFile &r;

	CMFCFile(CFile &r): r(r), errn(0) { }

	int errn;

	virtual bool	Close()
	{
		r.Close();
		return true;
	}
	virtual size_t	Read(void *buffer, size_t size, size_t count)
	{
		if (size != 0)
			return r.Read(buffer, size * count) / size;
		return 0;
	}
	virtual size_t	Write(const void *buffer, size_t size, size_t count)
	{
		errn = 1;
		return 0;
	}
	virtual bool	Seek(long offset, int origin)
	{
		if (r.Seek(offset, origin) == offset)
			return true;
		return false;
	}
	virtual long	Tell()
	{
		return r.Seek(0, CFile::current);
	}
	virtual long	Size()
	{
		return r.GetLength();
	}
	virtual bool	Flush()
	{
		r.Flush();
		return true;
	}
	virtual bool	Eof()
	{
		return r.GetPosition() >= r.GetLength();
	}
	virtual long	Error()
	{
		return errn;
	}
	virtual bool	PutC(unsigned char c)
		{
		// Default implementation
		size_t nWrote = Write(&c, 1, 1);
		return (bool)(nWrote == 1);
		}
	virtual long	GetC()
	{
		BYTE b;
		if (1 == Read(&b, 1, 1))
			return b;
		return -1;
	}
	virtual char *	GetS(char *string, int n)
	{
		ASSERT(false);
		errn = 1;
		return NULL;
	}
	virtual long	Scanf(const char *format, void* output)
	{
		ASSERT(false);
		return 0;
	}
};

void TIFFEH(const char*, const char*, va_list)
{

}

// CAxTIF3Doc シリアル化

void CAxTIF3Doc::Serialize(CArchive& ar)
{
	if (ar.IsStoring())
	{
		// TODO: 格納するコードをここに追加してください。
	}
	else
	{
		TIFFSetWarningHandler(TIFFEH);

		m_tifs.RemoveAll();

		CMFCFile fi(*ar.GetFile());
		int cx=1;
		for (int x=0; x<cx; x++) {
			CxImageTIF tif;
			tif.SetFrame(x);
			if (tif.Decode(&fi)) {
				cx = tif.GetNumFrames();
				CAutoPtr<CxImage> p(new CxImage());
				VERIFY(p->Transfer(tif));
				m_tifs.Add(p);
			}
			ar.GetFile()->SeekToBegin();
		}

		UpdateAllViews(NULL, UPHINT_LOADED);
	}
}


// CAxTIF3Doc 診断

#ifdef _DEBUG
void CAxTIF3Doc::AssertValid() const
{
	CDocument::AssertValid();
}

void CAxTIF3Doc::Dump(CDumpContext& dc) const
{
	CDocument::Dump(dc);
}
#endif //_DEBUG


// CAxTIF3Doc コマンド
