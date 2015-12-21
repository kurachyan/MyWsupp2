﻿using System;
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
            internal int frontpos;  // 前方キーワード 配置位置
            internal int backpos;   // 後方キーワード 配置位置
            internal String front;  // 前方キーワード
            internal String back;   // 後方キーワード
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
            {   // バッファーに実装有り
                Boolean _flg = false;	// 情報移行
                int _pos = 0;       // 位置情報
                int __pos = _pos;
                SepString sep = new SepString();
                String __wbuf;

                do
                {
                    _pos = _wbuf.IndexOf('\'', _pos);   // [’]検索
                    if (_pos != -1 && _flg == false)
                    {   // [’]有り？
                        sep.front = _wbuf.Substring(0, _pos + 1);   // 前方取得
                        sep.back = _wbuf.Substring(_pos + 2, _wbuf.Length - _pos - 2);      // 後方取得
                        _wbuf = sep.front + " " + sep.back;     // 再合成
                    }
                    else
                    {
                        _pos = __pos;
                    }

                    _pos = _wbuf.IndexOf('\"', _pos);   // [”]検索
                    if (_pos != -1)
                    {   // [”]有り？
                        if (_flg == false)
                        {   // 最初の［”］？
                            _pos++;
                            sep.frontpos = _pos;
                            __pos = _pos;
                            _flg = true;
                        }
                        else
                        {   // 最後の［”］？
                            sep.back = _wbuf.Substring(_pos, _wbuf.Length - _pos);   // 後方取得
                            sep.backpos = _pos;
                            sep.front = _wbuf.Substring(0, sep.frontpos);   // 前方取得
                            __wbuf = sep.front.PadRight(_pos);
                            _wbuf = __wbuf + sep.back;
                            _flg = false;
                        }
                    }
                    else
                    {
                        _pos = __pos;
                    }
                } while (_flg != false);

            }

        }
        #endregion
    }
}