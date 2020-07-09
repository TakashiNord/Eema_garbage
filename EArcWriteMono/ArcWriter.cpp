// ArcWriter.cpp : Defines the class behaviors for the application.
//

#include "stdafx.h"
#include "ArcWriter.h"
#include "version.h"
#include "subsyst.h"
#include <Controls\HyperLink.h>


#include "MainDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif


/////////////////////////////////////////////////////////////////////////////
// CAboutDlg dialog used for App About

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

	// Линк на домашнюю страницу
	CHyperLink m_linkHomePage;
	// Линк на почту
	CHyperLink m_linkEMail;

	// Dialog Data
	//{{AFX_DATA(CAboutDlg)
	enum { IDD = IDD_ABOUTBOX };
	CString	m_Version;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CAboutDlg)
protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

	// Implementation
protected:
	//{{AFX_MSG(CAboutDlg)
	virtual BOOL OnInitDialog();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
private:
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
	//{{AFX_DATA_INIT(CAboutDlg)
	m_Version.Format(_T("Версия %s от %s"),RSDU_PRODUCT_VERSION,__DATE__);;
	//}}AFX_DATA_INIT

}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CAboutDlg)
	DDX_Control(pDX, IDC_HOMEPAGELINK, m_linkHomePage);
	DDX_Control(pDX, IDC_EMAILLINK, m_linkEMail);
	DDX_Text(pDX, IDC_VERSION, m_Version);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
	//{{AFX_MSG_MAP(CAboutDlg)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

BOOL CAboutDlg::OnInitDialog() 
{
	CDialog::OnInitDialog();

	m_linkEMail.SetURL(_T("mailto:support@ema.ru"));
	m_linkEMail.SetUnderline(TRUE);
	m_linkEMail.SetLinkCursor(AfxGetApp()->LoadCursor(IDC_NEW_HAND));


	m_linkHomePage.SetURL(_T("http://www.ema.ru"));
	m_linkHomePage.SetUnderline(TRUE);
	m_linkHomePage.SetLinkCursor(AfxGetApp()->LoadCursor(IDC_NEW_HAND));

	return TRUE;  // return TRUE unless you set the focus to a control
	// EXCEPTION: OCX Property Pages should return FALSE
}

/////////////////////////////////////////////////////////////////////////////
// ArcWriterApp

BEGIN_MESSAGE_MAP(CArcWriterApp, CWinApp)
	//{{AFX_MSG_MAP(CArcWriterApp)
	ON_COMMAND(ID_APP_ABOUT, OnAppAbout)
		// NOTE - the ClassWizard will add and remove mapping macros here.
		//    DO NOT EDIT what you see in these blocks of generated code!
	//}}AFX_MSG_MAP
	// Standard file based document commands
	ON_COMMAND(ID_FILE_NEW, CWinApp::OnFileNew)
	ON_COMMAND(ID_FILE_OPEN, CWinApp::OnFileOpen)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CArcWriterApp construction

CArcWriterApp::CArcWriterApp() 
	: m_pIconInfo(NULL)
	, m_iIconInfoSize(0)
{	
	// TODO: add construction code here,
	// Place all significant initialization in InitInstance
}

CArcWriterApp::~CArcWriterApp()
{

	if (m_pIconInfo != NULL)
		delete []m_pIconInfo;
}

/////////////////////////////////////////////////////////////////////////////
// The one and only CArcWriterApp object

CArcWriterApp theApp;

/////////////////////////////////////////////////////////////////////////////
// CArcWriterApp initialization

BOOL CArcWriterApp::InitInstance()
{

	AfxEnableControlContainer();
	AfxOleInit(); 

	CString strSQL, strValue;
	if (GetUserInfo( &p_UserInfo ))
	{
		UID = p_UserInfo.ulUID;
		GID = p_UserInfo.ulGID;
	
		//Проверяем права пользователя
	    SubLaws *pSubSystemStand = p_UserInfo.stSubLaws;
		m_ulLawsBase = 0;

		int i = 0;
	    for(i = 0; i < 64; i++ )
		{
			//получаем маски прав для базовой подсистемы
			if ( pSubSystemStand[i].ulIdSubs == BASE )
			{
				m_ulLawsBase = pSubSystemStand[i].ulLaws;
				break;
			}
		}
		
		if ((m_ulLawsBase & BASE_STAND_READ)==0)
		{
			AfxMessageBox("Отсутствует роль для чтения базовой информации.\nОбратитесь к администратору.");
			return FALSE;
		}

		strSQL.Format("DSN=RSDU2;UID=%s;PWD=%s", p_UserInfo.Login, p_UserInfo.Password);
		
		try { m_db.OpenEx(strSQL);}

		catch(CDBException*)
		{	
			MessageBox(NULL, _T("Ошибка при открытии базы данных.\nОбратитесь, пожалуйста, к администратору.\n"), _T("Сообщение"), MB_ICONEXCLAMATION);
			return FALSE;
		}


		strSQL.Format("SET ROLE BASE_EXT_CONNECT_OIK");
		HSTMT hStmt;
		RETCODE retcode	= SQLAllocStmt(m_db.m_hdbc, &hStmt);
		if ((retcode!=SQL_SUCCESS) && (retcode!=SQL_SUCCESS_WITH_INFO)) 
		{
				return FALSE;
		}

		retcode = SQLExecDirect(hStmt, (unsigned char* )(LPCTSTR )strSQL, SQL_NTS);
		if (retcode != SQL_SUCCESS)
		{
			AfxMessageBox("Ошибка включения роли.");
		}
		SQLFreeStmt(hStmt,  SQL_CLOSE);

		//вызов процедуры (ID_APPL - идентификотор приложения)
        strSQL.Format("{? = call user_set_role('%s')} ",
		SZAPPL_NAME);
                
		SQLINTEGER nRetVal=0, iLength=0;
		char szError[256];
		for (i = 0; i < 256; i++)
			szError[i] = 0;

		SQLBindParameter(hStmt, 1, SQL_PARAM_OUTPUT, SQL_C_LONG, SQL_INTEGER, 0, 0, &nRetVal, 0, &iLength);
		 retcode = SQLExecDirect(hStmt, (unsigned char* )(LPCTSTR )strSQL, SQL_NTS);
		 if(retcode != SQL_SUCCESS && retcode != SQL_SUCCESS_WITH_INFO)
		 {
			AfxMessageBox("Ошибка запроса:\n" + strSQL + "\nSQLExecDirect");
			SQLFreeStmt(hStmt,  SQL_DROP);
			return ERR_NO_HDBC;
		 }
		if ( nRetVal!=0 )
		{
			strSQL.Format("{? = call RAISE_RSDU_ERROR('call user_set_role', %d)}", nRetVal);
			SQLBindParameter(hStmt, 1, SQL_PARAM_OUTPUT, SQL_C_CHAR, SQL_VARCHAR, 0, 0, szError, 255, &iLength);
			retcode = SQLExecDirect(hStmt, (unsigned char* )(LPCTSTR )strSQL, SQL_NTS);
			if(retcode != SQL_SUCCESS)
			{
				AfxMessageBox("Ошибка запроса:\n" + strSQL + "\nSQLAllocStmt");
				SQLFreeStmt(hStmt, SQL_DROP);
				return ERR_NO_LAWS;
			};
			SQLFreeStmt(hStmt,  SQL_RESET_PARAMS);
			CString strError;
			strError.Format("Ошибка при установке ролей.\n%s", szError);
			AfxMessageBox(strError, MB_OK, 0);
			return ERR_NO_LAWS;
		}
		 SQLFreeStmt(hStmt, SQL_CLOSE);
		 SQLFreeStmt(hStmt,  SQL_DROP);

		LoadStdProfileSettings(0);
	}
	else
		return FALSE;

	CString strCmdLine = m_lpCmdLine;	
	
	UINT	uiFlagExParam = 0;

	CMainDlg dlg;
	m_pMainWnd = &dlg;
	unsigned long ulIdAppl = 0;
	HWND hWnd = m_pMainWnd->m_hWnd;
	if (RSDUWinUtils_InitApplEx(SZAPPL_NAME, RSDU_PRODUCT_VERSION, (HWND)hWnd, RSDU_INIT_DEFAULT, &ulIdAppl, 
		RSDU_ERROR_SHOW | RSDU_ERROR_EVENTLOG) != RSDU_ERR_OK)
	{
		return FALSE;
	}

	INT_PTR nResponse = dlg.DoModal();
	if (nResponse == IDOK)
	{
		// TODO: Place code here to handle when the dialog is
		//  dismissed with OK
	}
	else if (nResponse == IDCANCEL)
	{
		// TODO: Place code here to handle when the dialog is
		//  dismissed with Cancel
	}
	RSDUWinUtils_ExitAppl(SZAPPL_NAME, (HWND)hWnd, RSDU_ERROR_EVENTLOG);

	// Since the dialog has been closed, return FALSE so that we exit the
	//  application, rather than start the application's message pump.
	return FALSE;
}


// App command to run the dialog
void CArcWriterApp::OnAppAbout()
{
	CAboutDlg aboutDlg;
	aboutDlg.DoModal();
}

/////////////////////////////////////////////////////////////////////////////
// CArcWriterApp message handlers


BOOL CArcWriterApp::GetUserInfo(CurrentUser *p_UserInfo)
{

	HANDLE hAMap;
	CString Msg;
	CurrentUser *p_CurrentUser = NULL;

    hAMap= OpenFileMapping(FILE_MAP_READ | FILE_MAP_WRITE, TRUE, CURRENT_USER_INFO);

    if (hAMap == NULL)
    {
		AfxMessageBox("Ошибка получения прав в следствие:\n1.Не запущена панель приложений\n2.Отсутствует информация о пользователе\n3.Что-то не так в Датском королевстве.\nОбратитесь к администратору");
		return FALSE;
	}

	p_CurrentUser = (CurrentUser *)MapViewOfFile(hAMap, FILE_MAP_WRITE | FILE_MAP_READ, 0, 0, 4096);
    
	if (p_CurrentUser == NULL)
    {
		AfxMessageBox("Ошибка получения прав в следствие:\n1.Отсутствует информация о пользователе\n2.Что-то не так в Датском королевстве.\nОбратитесь к разработчику");
		if (hAMap) CloseHandle (hAMap);
		return FALSE;
	}

	memmove((void *)p_UserInfo, (void *)p_CurrentUser, p_CurrentUser->ulSize);
    // Unmap view, close mapping!
    
	if (p_CurrentUser)
    {
       UnmapViewOfFile (p_CurrentUser);
       p_CurrentUser = NULL;
    }
    
	if (hAMap)
    {
		CloseHandle (hAMap);
        hAMap = NULL;
    }
	
	return TRUE;
}

int CArcWriterApp::ExitInstance() 
{
	CString strTmp;
	CString strReg;
	HKEY hKey = NULL;
 	m_db.Close();	

	if (strcmp(p_UserInfo.Login,"")==0)
		return CWinApp::ExitInstance();

	return CWinApp::ExitInstance();
}

BOOL CArcWriterApp::SetAllRoles()
{
	/*надо роли дать..*/
	CString strSQL;
	HSTMT hStmt;

	/* устанавливаем роли по-новой */
	strSQL.Format("SET ROLE BASE_EXT_CONNECT_OIK");
	RETCODE retcode	= SQLAllocStmt(m_db.m_hdbc, &hStmt);
	if ((retcode!=SQL_SUCCESS) && (retcode!=SQL_SUCCESS_WITH_INFO)) {
		return FALSE;
	}

	retcode = SQLExecDirect(hStmt, (unsigned char* )(LPCTSTR )strSQL, SQL_NTS);
	if (retcode != SQL_SUCCESS)
	{
		AfxMessageBox("Ошибка включения роли.");
	}
	SQLFreeStmt(hStmt,  SQL_CLOSE);

	//вызов процедуры (алиас приложения)
	strSQL.Format("{? = call user_set_role('%s')} ",
		SZAPPL_NAME);

	SQLINTEGER nRetVal=0, iLength=0;
	char szError[256];
	for ( int i = 0; i<256; i++ ) szError[i] = 0;
	SQLBindParameter(hStmt, 1, SQL_PARAM_OUTPUT, SQL_C_LONG, SQL_INTEGER, 0, 0, &nRetVal, 0, &iLength);
	retcode = SQLExecDirect(hStmt, (unsigned char* )(LPCTSTR )strSQL, SQL_NTS);
	if(retcode != SQL_SUCCESS && retcode != SQL_SUCCESS_WITH_INFO)
	{
		AfxMessageBox("Ошибка запроса:\n" + strSQL + "\nSQLExecDirect");
		SQLFreeStmt(hStmt,  SQL_DROP);
		return ERR_NO_HDBC;
	}
	if ( nRetVal!=0 )
	{
		strSQL.Format("{? = call RAISE_RSDU_ERROR('call user_set_role', %d)}", nRetVal);
		SQLBindParameter(hStmt, 1, SQL_PARAM_OUTPUT, SQL_C_CHAR, SQL_VARCHAR, 0, 0, szError, 255, &iLength);
		retcode = SQLExecDirect(hStmt, (unsigned char* )(LPCTSTR )strSQL, SQL_NTS);
		if(retcode != SQL_SUCCESS)
		{
			AfxMessageBox("Ошибка запроса:\n" + strSQL + "\nSQLAllocStmt");
			SQLFreeStmt(hStmt, SQL_DROP);
			return ERR_NO_LAWS;
		};
		SQLFreeStmt(hStmt,  SQL_RESET_PARAMS);
		CString strError;
		strError.Format("Ошибка при установке ролей.\n%s", szError);
		AfxMessageBox(strError, MB_OK, 0);
		return ERR_NO_LAWS;
	}
	SQLFreeStmt(hStmt, SQL_CLOSE);
	SQLFreeStmt(hStmt,  SQL_DROP);


	/* установка роли в библиотеке*/
	if (RSDUWinUtils_SetRole(SZAPPL_NAME,RSDU_ERROR_EVENTLOG)!=RSDU_ERR_OK)
	{
		AfxMessageBox("Ошибка установки роли:\n" + strSQL);
		return false;

	}
	return TRUE;	 
}

