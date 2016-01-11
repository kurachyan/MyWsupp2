using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LRSkip;

namespace Wsupp
{
    public class CS_Wsupp
    {
        #region 共有領域
        CS_Rskip rskip;             // 右側余白情報を削除
        CS_Lskip lskip;             // 左側余白情報を削除

        struct SepString
        {   // 文字列分割管理
            internal Boolean sepflg;    // 分割情報利用有無
            internal Boolean chrstr;    // [false : char] [true : string]
            internal int frontpos;  // 前方キーワード 配置位置
            internal int backpos;   // 後方キーワード 配置位置
            internal String front;  // 前方キーワード
            internal String back;   // 後方キーワード
            internal String spc;    // 空白設定情報
        }
        private String _wbuf;       // ソース情報
        private Boolean _empty;     // ソース情報有無
        public String Wbuf
        {
            get
            {
                return (_wbuf);
            }
            set
            {
                _wbuf = value;
                if (_wbuf == null)
                {   // 設定情報は無し？
                    _empty = true;
                }
                else
                {   // 整形処理を行う
                    // 不要情報削除
                    if (rskip == null || lskip == null)
                    {   // 未定義？
                        rskip = new CS_Rskip();
                        lskip = new CS_Lskip();
                    }
                    rskip.Wbuf = _wbuf;
                    rskip.Exec();
                    lskip.Wbuf = rskip.Wbuf;
                    lskip.Exec();
                    _wbuf = lskip.Wbuf;

                    // 作業の為の下処理
                    if (_wbuf.Length == 0 || _wbuf == null)
                    {   // バッファー情報無し
                        // _wbuf = null;
                        _empty = true;
                    }
                    else
                    {
                        _empty = false;
                    }
                }
            }
        }
        #endregion

        #region コンストラクタ
        public CS_Wsupp()
        {   // コンストラクタ
            _wbuf = null;       // 設定情報無し
            _empty = true;

            rskip = null;
            lskip = null;

        }
        #endregion

        #region モジュール
        public void Clear()
        {   // 作業領域の初期化
            _wbuf = null;       // 設定情報無し
            _empty = true;

            rskip = null;
            lskip = null;

        }
        public void Exec()
        {   // 引用消去を行う
            if (!_empty)
            {   // バッファーに実装有
                Boolean _flg = true;   // 情報移行
                SepString sep = new SepString();

                sep.sepflg = false;     // 未検出
                for (int i = 0; i < _wbuf.Length; i++)
                {   // 文字長分、処理を繰り返す
                    if ((_wbuf[i] == '\'') && (_flg == true))
                    {   // \'検出：入り口
                        _flg = false;
                        sep.chrstr = false;     // [Char]
                        sep.frontpos = i;
                        sep.spc = "";
                        sep.sepflg = true;      // 取り込み開始
                        continue;
                    }
                    if ((_wbuf[i] == '\'') && (_flg == false))
                    {   // \'検出：出口
                        if (sep.chrstr == false)
                        {   // [Char]確認？
                            sep.backpos = i;

                            sep.front = _wbuf.Substring(0, sep.frontpos + 1);   // 前方取得
                            sep.back = _wbuf.Substring(sep.backpos, _wbuf.Length - sep.backpos);      // 後方取得
                            _wbuf = sep.front + sep.spc + sep.back;     // 再合成

                            _flg = true;
                            sep.sepflg = false;      // 取り込み終了
                            continue;
                        }
                    }

                    if ((_wbuf[i] == '\"') && (_flg == true))
                    {   // \"検出：入り口
                        _flg = false;
                        sep.chrstr = true;     // [String]
                        sep.frontpos = i;
                        sep.spc = "";
                        sep.sepflg = true;      // 取り込み開始
                        continue;
                    }
                    if ((_wbuf[i] == '\"') && (_flg == false))
                    {   // \"検出：出口
                        if (sep.chrstr == true)
                        {   // [string]確認？
                            sep.backpos = i;

                            sep.front = _wbuf.Substring(0, sep.frontpos + 1);   // 前方取得
                            sep.back = _wbuf.Substring(sep.backpos, _wbuf.Length - sep.backpos);      // 後方取得
                            _wbuf = sep.front + sep.spc + sep.back;     // 再合成

                            _flg = true;
                            sep.sepflg = false;      // 取り込み終了
                            sep.spc = "";
                            continue;
                        }
                    }

                    if (_flg == false)
                    {
                        sep.spc += " ";             // 文字情報を削除
                    }
                }
            }
        }

        public void Exec(String msg)
        {   // 引用消去を行う
            Setbuf(msg);                 // 入力内容の作業領域設定

            if (!_empty)
            {   // バッファーに実装有
                Boolean _flg = true;   // 情報移行
                SepString sep = new SepString();

                sep.sepflg = false;     // 未検出
                for (int i = 0; i < _wbuf.Length; i++)
                {   // 文字長分、処理を繰り返す
                    if ((_wbuf[i] == '\'') && (_flg == true))
                    {   // \'検出：入り口
                        _flg = false;
                        sep.chrstr = false;     // [Char]
                        sep.frontpos = i;
                        sep.spc = "";
                        sep.sepflg = true;      // 取り込み開始
                        continue;
                    }
                    if ((_wbuf[i] == '\'') && (_flg == false))
                    {   // \'検出：出口
                        if (sep.chrstr == false)
                        {   // [Char]確認？
                            sep.backpos = i;

                            sep.front = _wbuf.Substring(0, sep.frontpos + 1);   // 前方取得
                            sep.back = _wbuf.Substring(sep.backpos, _wbuf.Length - sep.backpos);      // 後方取得
                            _wbuf = sep.front + sep.spc + sep.back;     // 再合成

                            _flg = true;
                            sep.sepflg = false;      // 取り込み終了
                            continue;
                        }
                    }

                    if ((_wbuf[i] == '\"') && (_flg == true))
                    {   // \"検出：入り口
                        _flg = false;
                        sep.chrstr = true;     // [String]
                        sep.frontpos = i;
                        sep.spc = "";
                        sep.sepflg = true;      // 取り込み開始
                        continue;
                    }
                    if ((_wbuf[i] == '\"') && (_flg == false))
                    {   // \"検出：出口
                        if (sep.chrstr == true)
                        {   // [string]確認？
                            sep.backpos = i;

                            sep.front = _wbuf.Substring(0, sep.frontpos + 1);   // 前方取得
                            sep.back = _wbuf.Substring(sep.backpos, _wbuf.Length - sep.backpos);      // 後方取得
                            _wbuf = sep.front + sep.spc + sep.back;     // 再合成

                            _flg = true;
                            sep.sepflg = false;      // 取り込み終了
                            sep.spc = "";
                            continue;
                        }
                    }

                    if (_flg == false)
                    {
                        sep.spc += " ";             // 文字情報を削除
                    }
                }
            }
        }

        private void Setbuf(String _strbuf)
        {   // [_wbuf]情報設定
            _wbuf = _strbuf;
            if (_wbuf == null)
            {   // 設定情報は無し？
                _empty = true;
            }
            else
            {   // 整形処理を行う
                // 不要情報削除
                if (rskip == null || lskip == null)
                {   // 未定義？
                    rskip = new CS_Rskip();
                    lskip = new CS_Lskip();
                }
                rskip.Wbuf = _wbuf;
                rskip.Exec();
                lskip.Wbuf = rskip.Wbuf;
                lskip.Exec();
                _wbuf = lskip.Wbuf;

                // 作業の為の下処理
                if (_wbuf.Length == 0 || _wbuf == null)
                {   // バッファー情報無し
                    // _wbuf = null;
                    _empty = true;
                }
                else
                {
                    _empty = false;
                }
            }
        }
        #endregion
    }
}
