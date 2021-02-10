Option Strict Off
Option Explicit On
Friend Class frmMain
	Inherits System.Windows.Forms.Form

	'Private Sub chkEquipes_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkEquipes.CheckStateChanged

	'	optEquipes(0).Enabled = (chkEquipes.CheckState = 1)
	'	optEquipes(1).Enabled = (chkEquipes.CheckState = 1)

	'End Sub

	Private Sub Command1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command1.Click
		Dim rsTeams As ADODB.Recordset
		Dim I As Short
		Dim strPath As String
		
		strPath = My.Application.Info.DirectoryPath
		
		lblAction.Text = "Connection � la base de donn�es..."
		System.Windows.Forms.Application.DoEvents()
		Call ConnectToDatabase(strPath)
		
		If chkCompteurs.CheckState = 1 Then
			lblAction.Text = "Cr�ation de la page : Compteurs"
			System.Windows.Forms.Application.DoEvents()
			Call subOutputCompteurs("Compteurs s�ries " & YEAR_Renamed, strPath & "\SiteWebTemp\compteurs.htm")
		End If
		
		If chkHoraire.CheckState = 1 Then
			lblAction.Text = "Cr�ation de la page : Calendrier"
			System.Windows.Forms.Application.DoEvents()
			Call subOutputCalendrier(-1, "Calendrier " & YEAR_Renamed, strPath & "\SiteWebTemp\calendrier.htm")
		End If
		
		If chkClassement.CheckState = 1 Then
			lblAction.Text = "Cr�ation de la page : Classement"
			System.Windows.Forms.Application.DoEvents()
			Call subOutputClassement("Classement s�ries " & YEAR_Renamed, strPath & "\SiteWebTemp\classement.htm")
		End If
		
		If chkEquipes.CheckState = 1 Then
			If opt2�quipe.Checked Then 'Une �quipe
				rsTeams = rsGetTeams(False)
				rsTeams.MoveFirst()
				lblAction.Text = "Cr�ation de la page d'�quipe : " & rsTeams.Fields(1).Value
				System.Windows.Forms.Application.DoEvents()
				Call subOutputEquipe(rsTeams.Fields(0).Value, rsTeams.Fields(1).Value, strPath & "\SiteWebTemp\equipe_" & Trim(Str(rsTeams.Fields(0).Value)) & ".htm")
				lblAction.Text = "Cr�ation de la page d'�quipe : " & rsTeams.Fields(3).Value
				System.Windows.Forms.Application.DoEvents()
				Call subOutputEquipe(rsTeams.Fields(2).Value, rsTeams.Fields(3).Value, strPath & "\SiteWebTemp\equipe_" & Trim(Str(rsTeams.Fields(2).Value)) & ".htm")
			Else 'Toutes les �quipes
				rsTeams = rsGetTeams(True)
				rsTeams.MoveFirst()
				While Not rsTeams.EOF
					lblAction.Text = "Cr�ation de la page d'�quipe : " & rsTeams.Fields(1).Value
					System.Windows.Forms.Application.DoEvents()
					Call subOutputEquipe(rsTeams.Fields(0).Value, rsTeams.Fields(1).Value, strPath & "\SiteWebTemp\equipe_" & Trim(Str(rsTeams.Fields(0).Value)) & ".htm")
					rsTeams.MoveNext()
				End While
			End If
		End If
		
		gcConn.Close()
		lblAction.Text = ""
		MsgBox("Termin�! Le fichier est pr�t dans " & strPath & "\SiteWebTemp")

	End Sub
	
	
	Private Sub Command3_Click()
		MsgBox(My.Application.Info.DirectoryPath)
	End Sub
	
	Private Sub frmMain_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		
		chkHoraire.CheckState = System.Windows.Forms.CheckState.Checked
		chkClassement.CheckState = System.Windows.Forms.CheckState.Checked
		chkCompteurs.CheckState = System.Windows.Forms.CheckState.Checked
		chkEquipes.CheckState = System.Windows.Forms.CheckState.Checked
		optToutesLes�quipes.Checked = True

	End Sub

	Private Sub _optEquipes_0_CheckedChanged(sender As Object, e As EventArgs)

	End Sub
End Class