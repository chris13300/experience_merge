Module modMain

    Sub Main()
        Dim tabFichier() As Byte, tabExperience(0) As Byte, tailleFichier As Long, index As Long
        Dim maxTaille As Long, maxTailleFichier As String, nbFichiers As Integer, compteur As Integer

        index = 0
        maxTaille = 0
        maxTailleFichier = ""
        nbFichiers = My.Computer.FileSystem.GetFiles(My.Application.Info.DirectoryPath, FileIO.SearchOption.SearchTopLevelOnly, "*.exp").Count
        compteur = 0

        For Each fichier In My.Computer.FileSystem.GetFiles(My.Application.Info.DirectoryPath, FileIO.SearchOption.SearchTopLevelOnly, "*.exp")
            tabFichier = My.Computer.FileSystem.ReadAllBytes(fichier)

            tailleFichier = FileLen(fichier)
            If tailleFichier > maxTaille Then
                maxTaille = tailleFichier
                maxTailleFichier = fichier
            End If
            If index = 0 Then
                ReDim Preserve tabExperience(tailleFichier - 1)
                Array.Copy(tabFichier, 0, tabExperience, 0, tailleFichier)
                index = index + tailleFichier
            Else
                ReDim Preserve tabExperience(UBound(tabExperience) + tailleFichier - 26)
                Array.Copy(tabFichier, 26, tabExperience, index, tailleFichier - 26)
                index = index + tailleFichier - 26
            End If
            compteur = compteur + 1
            Console.Clear()
            Console.WriteLine("merging @ " & Format(compteur / nbFichiers, "0.00%") & ", " & My.Computer.FileSystem.GetFileInfo(fichier).Name)
        Next

        If maxTailleFichier <> "" Then
            Console.WriteLine("writing " & Trim(Format(tabExperience.Length / 1024 / 1024, "# ##0")) & " MB on " & My.Computer.FileSystem.GetFileInfo(maxTailleFichier).Name)
            My.Computer.FileSystem.WriteAllBytes(maxTailleFichier, tabExperience, False)

            For Each fichier In My.Computer.FileSystem.GetFiles(My.Application.Info.DirectoryPath, FileIO.SearchOption.SearchTopLevelOnly, "*.exp")
                If fichier <> maxTailleFichier Then
                    My.Computer.FileSystem.DeleteFile(fichier, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                End If
            Next
        End If
    End Sub

End Module
