############################################################################################################
                                             SYSTEM CONFIGURATION
############################################################################################################

Wii/Properties/Setting.settings

#Config SQL Connection Fields: 
CONNECT_Server
CONNECT_DBName
CONNECT_UserID
CONNECT_Password
CONNECT_DDETimeout
CONNECT_DDETimeout_CheckConnecting

#Config moudule 3DSTVOutput Fields: 
ThreeDSTSV_出力パス 
ThreeDSTSV_ランキングボード用パス 
ThreeDSTSV_歌手変更履歴取得元歌手TSVパス
ThreeDSTSV_歌手取込配置ファイル 
ThreeDSTSV_歌手変更履歴取得元コンテンツTSVパス 
ThreeDSTSV_コンテンツ取込配置ファイル 
ThreeDSTSV_変換バッチファイル 
ThreeDSTSV_変換バッチログファイル 
ThreeDSTSV_ログファイル 
ThreeDSTSV_収集リストパス 

#Config moudule UTSV Fields:
UTSV出力_おすすめ曲 
UTSV出力_ナビジャンル曲表 
UTSV出力_ナビジャンル歌手表 
UTSV出力_ナビ楽曲マスタ表 
UTSV出力_ナビ選曲番号表
UTSV出力_ランキングファイル 
UTSV出力_ログファイル 
UTSV出力_入力パス
UTSV出力_出力パス 
UTSV出力_年間ランキング
UTSV出力_歌手削除チェックファイルパス 
UTSV出力_比較元歌手TSVパス 
UTSV出力_総合ランキング 
UTSVRecommendSongServerFilePath 

############################################################################################################
                                          PROJECT STRUCTURE
############################################################################################################

Wii Project:
	|_Utilities ## プロジェクトで使用されるユーティリティ
		|_LogWriter.cs ## データを入力形式でデータをファイルに保存する処理
		|_TsvConvert.cs ## データをファイルにエクスポートする前の処理
		|_Utils.cs ## 多箇所で使用されるユーティリティ関数
	|_Wii	## ユーザーインターフェース画面
		|_UC
		 |_UCSqlConnection.cs ## プログラミング起動時のSQL接続チェックインターフェイス
		 |_CircularProgressBar.cs ## スタンドバイ状態のControl
		 |_DSTSVOutput.cs ## 3DSTVOutput　画面
		 |_ImportRanking.cs ## ImportRanking　画面
		 |_ImportRecommendSong.cs ## ImportRecommendSong　画面
		 |_OrchTSV.cs ## OrchTSV　画面
		 |_Program.cs ## プログラムのメイン関数、プログラムの最初の実行インターフェイスを呼び出す
		 |_SearchByID.cs ## 曲IDで検索画面
		 |_WaitingForm.cs ## 長時間実行機能のスタンバイ画面
		 |_WiiMain.cs ## 起動時のプログラムのメイン画面
		 |_WiiSystemBase.cs ## Wiiプロジェクトで使用される画面の継承使用継承のクラスmain
		 |_Settings.settings ## システム構成パラメーターを保存する
	|_Wii Common ## システム全体で使用されるプロセス
		 |_GetResources.cs ## それぞれの画面でリソースからメッセージを取得して各機能を表示する
		 |_WiiConstant.cs ## システムのデフォルトパラメータを定義する
	|_WiiController ## 各機能の業務処理
		|_Lib
			|_Microsoft.ApplicationBlocks.Data.dll ## データベース接続関数を使用するライブラリ
		 |_GetConnection.cs ## データベース接続チェックを処理し、データベース接続文字列を保存します
		 |_ImportRankingController.cs ## ImportRanking機能業務
		 |_ImportRecommendSongController.cs ## ImportRecommend機能業務
		 |_OrchTSCVController.cs ## OrchTSCV機能業務
		 |_ThreeDSTSVController.cs ## ThreeDSTSV機能業務
	|_WiiObjects ## プロジェクトで使用されるプロパティ
		 |_ErrorEntity.cs ## システムエラー情報を保存する
		 |_FilesCollection.cs ## プロジェクトのファイルリストを処理する
		 |_ItemObject.cs ## 曲の検索情報を保存するプロパティ
		 |_ParameterObjet.cs ## パラメーターテーブルの値を保存するプロパティ
		 |_UtsvLabelObject.cs ## UtsvLabelテーブルの値を保存するプロパティ
	|_WiiSetup ## プロジェクトのインストーラーを作成する
