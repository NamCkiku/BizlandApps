﻿using System.Collections.Generic;

namespace Bizland.Model
{
    public class Country
    {
        public string IsoCode { get; set; }
        public string Name { get; set; }
        public string DialCode { get; set; }
        public string FlagPath { get; set; }
        public string NameSort
        {
            get
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    return Name[0].ToString().ToUpper();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public static List<Country> All
        {
            get
            {
                return CountryList;
            }
        }

        public static List<string> AllNames
        {
            get
            {
                List<Country> countryList = CountryList;
                List<string> countryNames = new List<string>();
                foreach (var item in countryList)
                {
                    countryNames.Add(item.Name);
                }

                return countryNames;
            }
        }

        public Country(string IsoCode, string Name, string DialCode, string FlagPath)
        {
            this.IsoCode = IsoCode;
            this.Name = Name;
            this.DialCode = DialCode;
            this.FlagPath = FlagPath;
        }

        public static Country GetCountryByISO(string ISO)
        {
            int foundIndex = -1;
            foreach (var item in CountryList)
            {
                if (item.IsoCode == ISO)
                {
                    foundIndex = CountryList.IndexOf(item);
                }
            }

            if (foundIndex != -1)
            {
                Country foundCountry = CountryList[foundIndex];
                return foundCountry;
            }
            else
            {
                return null;
            }
        }

        private static readonly List<Country> CountryList = new List<Country> {
            new Country("AD", "Andorra", "+376", "flag_ad.png"),
            new Country("AE", "United Arab Emirates", "+971", "flag_ae.png"),
            new Country("AF", "Afghanistan", "+93", "flag_af.png"),
            new Country("AG", "Antigua and Barbuda", "+1", "flag_ag.png"),
            new Country("AI", "Anguilla", "+1", "flag_ai.png"),
            new Country("AL", "Albania", "+355", "flag_al.png"),
            new Country("AM", "Armenia", "+374", "flag_am.png"),
            new Country("AO", "Angola", "+244", "flag_ao.png"),
            new Country("AQ", "Antarctica", "+672", "flag_aq.png"),
            new Country("AR", "Argentina", "+54", "flag_ar.png"),
            new Country("AS", "AmericanSamoa", "+1", "flag_as.png"),
            new Country("AT", "Austria", "+43", "flag_at.png"),
            new Country("AU", "Australia", "+61", "flag_au.png"),
            new Country("AW", "Aruba", "+297", "flag_aw.png"),
            new Country("AX", "Åland Islands", "+358", "flag_ax.png"),
            new Country("AZ", "Azerbaijan", "+994", "flag_az.png"),
            new Country("BA", "Bosnia and Herzegovina", "+387", "flag_ba.png"),
            new Country("BB", "Barbados", "+1", "flag_bb.png"),
            new Country("BD", "Bangladesh", "+880", "flag_bd.png"),
            new Country("BE", "Belgium", "+32", "flag_be.png"),
            new Country("BF", "Burkina Faso", "+226", "flag_bf.png"),
            new Country("BG", "Bulgaria", "+359", "flag_bg.png"),
            new Country("BH", "Bahrain", "+973", "flag_bh.png"),
            new Country("BI", "Burundi", "+257", "flag_bi.png"),
            new Country("BJ", "Benin", "+229", "flag_bj.png"),
            new Country("BL", "Saint Barthélemy", "+590", "flag_bl.png"),
            new Country("BM", "Bermuda", "+1", "flag_bm.png"),
            new Country("BN", "Brunei Darussalam", "+673", "flag_bn.png"),
            new Country("BO", "Bolivia", "+591", "flag_bo.png"), // , Plurinational State of
            new Country("BQ", "Bonaire", "+599", "flag_bq.png"),
            new Country("BR", "Brazil", "+55", "flag_br.png"),
            new Country("BS", "Bahamas", "+1", "flag_bs.png"),
            new Country("BT", "Bhutan", "+975", "flag_bt.png"),
            new Country("BV", "Bouvet Island", "+47", "flag_bv.png"),
            new Country("BW", "Botswana", "+267", "flag_bw.png"),
            new Country("BY", "Belarus", "+375", "flag_by.png"),
            new Country("BZ", "Belize", "+501", "flag_bz.png"),
            new Country("CA", "Canada", "+1", "flag_ca.png"),
            new Country("CC", "Cocos (Keeling) Islands", "+61", "flag_cc.png"),
            new Country("CD", "Congo", "+243", "flag_cd.png"), // , The Democratic Republic of the
            new Country("CF", "Central African Republic", "+236", "flag_cf.png"),
            new Country("CG", "Congo", "+242", "flag_cg.png"),
            new Country("CH", "Switzerland", "+41", "flag_ch.png"),
            new Country("CI", "Ivory Coast", "+225", "flag_ci.png"),
            new Country("CK", "Cook Islands", "+682", "flag_ck.png"),
            new Country("CL", "Chile", "+56", "flag_cl.png"),
            new Country("CM", "Cameroon", "+237", "flag_cm.png"),
            new Country("CN", "China", "+86", "flag_cn.png"),
            new Country("CO", "Colombia", "+57", "flag_co.png"),
            new Country("CR", "Costa Rica", "+506", "flag_cr.png"),
            new Country("CU", "Cuba", "+53", "flag_cu.png"),
            new Country("CV", "Cape Verde", "+238", "flag_cv.png"),
            new Country("CW", "Curacao", "+599", "flag_cw.png"),
            new Country("CX", "Christmas Island", "+61", "flag_cx.png"),
            new Country("CY", "Cyprus", "+357", "flag_cy.png"),
            new Country("CZ", "Czech Republic", "+420", "flag_cz.png"),
            new Country("DE", "Germany", "+49", "flag_de.png"),
            new Country("DJ", "Djibouti", "+253", "flag_dj.png"),
            new Country("DK", "Denmark", "+45", "flag_dk.png"),
            new Country("DM", "Dominica", "+1", "flag_dm.png"),
            new Country("DO", "Dominican Republic", "+1", "flag_do.png"),
            new Country("DZ", "Algeria", "+213", "flag_dz.png"),
            new Country("EC", "Ecuador", "+593", "flag_ec.png"),
            new Country("EE", "Estonia", "+372", "flag_ee.png"),
            new Country("EG", "Egypt", "+20", "flag_eg.png"),
            new Country("EH", "Western Sahara", "+212", "flag_eh.png"),
            new Country("ER", "Eritrea", "+291", "flag_er.png"),
            new Country("ES", "Spain", "+34", "flag_es.png"),
            new Country("ET", "Ethiopia", "+251", "flag_et.png"),
            new Country("FI", "Finland", "+358", "flag_fi.png"),
            new Country("FJ", "Fiji", "+679", "flag_fj.png"),
            new Country("FK", "Falkland Islands (Malvinas)", "+500", "flag_fk.png"),
            new Country("FM", "Micronesia", "+691", "flag_fm.png"), // , Federated States of
            new Country("FO", "Faroe Islands", "+298", "flag_fo.png"),
            new Country("FR", "France", "+33", "flag_fr.png"),
            new Country("GA", "Gabon", "+241", "flag_ga.png"),
            new Country("GB", "United Kingdom", "+44", "flag_gb.png"),
            new Country("GD", "Grenada", "+1", "flag_gd.png"),
            new Country("GE", "Georgia", "+995", "flag_ge.png"),
            new Country("GF", "French Guiana", "+594", "flag_gf.png"),
            new Country("GG", "Guernsey", "+44", "flag_gg.png"),
            new Country("GH", "Ghana", "+233", "flag_gh.png"),
            new Country("GI", "Gibraltar", "+350", "flag_gi.png"),
            new Country("GL", "Greenland", "+299", "flag_gl.png"),
            new Country("GM", "Gambia", "+220", "flag_gm.png"),
            new Country("GN", "Guinea", "+224", "flag_gn.png"),
            new Country("GP", "Guadeloupe", "+590", "flag_gp.png"),
            new Country("GQ", "Equatorial Guinea", "+240", "flag_gq.png"),
            new Country("GR", "Greece", "+30", "flag_gr.png"),
            new Country("GS", "South Georgia and the South Sandwich Islands", "+500", "flag_gs.png"),
            new Country("GT", "Guatemala", "+502", "flag_gt.png"),
            new Country("GU", "Guam", "+1", "flag_gu.png"),
            new Country("GW", "Guinea-Bissau", "+245", "flag_gw.png"),
            new Country("GY", "Guyana", "+595", "flag_gy.png"),
            new Country("HK", "Hong Kong", "+852", "flag_hk.png"),
            new Country("HM", "Heard Island and McDonald Islands", "", "flag_hm.png"),
            new Country("HN", "Honduras", "+504", "flag_hn.png"),
            new Country("HR", "Croatia", "+385", "flag_hr.png"),
            new Country("HT", "Haiti", "+509", "flag_ht.png"),
            new Country("HU", "Hungary", "+36", "flag_hu.png"),
            new Country("ID", "Indonesia", "+62", "flag_id.png"),
            new Country("IE", "Ireland", "+353", "flag_ie.png"),
            new Country("IL", "Israel", "+972", "flag_il.png"),
            new Country("IM", "Isle of Man", "+44", "flag_im.png"),
            new Country("IN", "India", "+91", "flag_in.png"),
            new Country("IO", "British Indian Ocean Territory", "+246", "flag_io.png"),
            new Country("IQ", "Iraq", "+964", "flag_iq.png"),
            new Country("IR", "Iran, Islamic Republic of", "+98", "flag_ir.png"),
            new Country("IS", "Iceland", "+354", "flag_is.png"),
            new Country("IT", "Italy", "+39", "flag_it.png"),
            new Country("JE", "Jersey", "+44", "flag_je.png"),
            new Country("JM", "Jamaica", "+1", "flag_jm.png"),
            new Country("JO", "Jordan", "+962", "flag_jo.png"),
            new Country("JP", "Japan", "+81", "flag_jp.png"),
            new Country("KE", "Kenya", "+254", "flag_ke.png"),
            new Country("KG", "Kyrgyzstan", "+996", "flag_kg.png"),
            new Country("KH", "Cambodia", "+855", "flag_kh.png"),
            new Country("KI", "Kiribati", "+686", "flag_ki.png"),
            new Country("KM", "Comoros", "+269", "flag_km.png"),
            new Country("KN", "Saint Kitts and Nevis", "+1", "flag_kn.png"),
            new Country("KP", "North Korea", "+850", "flag_kp.png"),
            new Country("KR", "South Korea", "+82", "flag_kr.png"),
            new Country("KW", "Kuwait", "+965", "flag_kw.png"),
            new Country("KY", "Cayman Islands", "+345", "flag_ky.png"),
            new Country("KZ", "Kazakhstan", "+7", "flag_kz.png"),
            new Country("LA", "Lao People's Democratic Republic", "+856", "flag_la.png"),
            new Country("LB", "Lebanon", "+961", "flag_lb.png"),
            new Country("LC", "Saint Lucia", "+1", "flag_lc.png"),
            new Country("LI", "Liechtenstein", "+423", "flag_li.png"),
            new Country("LK", "Sri Lanka", "+94", "flag_lk.png"),
            new Country("LR", "Liberia", "+231", "flag_lr.png"),
            new Country("LS", "Lesotho", "+266", "flag_ls.png"),
            new Country("LT", "Lithuania", "+370", "flag_lt.png"),
            new Country("LU", "Luxembourg", "+352", "flag_lu.png"),
            new Country("LV", "Latvia", "+371", "flag_lv.png"),
            new Country("LY", "Libyan Arab Jamahiriya", "+218", "flag_ly.png"),
            new Country("MA", "Morocco", "+212", "flag_ma.png"),
            new Country("MC", "Monaco", "+377", "flag_mc.png"),
            new Country("MD", "Moldova", "+373", "flag_md.png"), // , Republic of
            new Country("ME", "Montenegro", "+382", "flag_me.png"),
            new Country("MF", "Saint Martin", "+590", "flag_mf.png"),
            new Country("MG", "Madagascar", "+261", "flag_mg.png"),
            new Country("MH", "Marshall Islands", "+692", "flag_mh.png"),
            new Country("MK", "Macedonia", "+389", "flag_mk.png"), // , The Former Yugoslav Republic of
            new Country("ML", "Mali", "+223", "flag_ml.png"),
            new Country("MM", "Myanmar", "+95", "flag_mm.png"),
            new Country("MN", "Mongolia", "+976", "flag_mn.png"),
            new Country("MO", "Macao", "+853", "flag_mo.png"),
            new Country("MP", "Northern Mariana Islands", "+1", "flag_mp.png"),
            new Country("MQ", "Martinique", "+596", "flag_mq.png"),
            new Country("MR", "Mauritania", "+222", "flag_mr.png"),
            new Country("MS", "Montserrat", "+1", "flag_ms.png"),
            new Country("MT", "Malta", "+356", "flag_mt.png"),
            new Country("MU", "Mauritius", "+230", "flag_mu.png"),
            new Country("MV", "Maldives", "+960", "flag_mv.png"),
            new Country("MW", "Malawi", "+265", "flag_mw.png"),
            new Country("MX", "Mexico", "+52", "flag_mx.png"),
            new Country("MY", "Malaysia", "+60", "flag_my.png"),
            new Country("MZ", "Mozambique", "+258", "flag_mz.png"),
            new Country("NA", "Namibia", "+264", "flag_na.png"),
            new Country("NC", "New Caledonia", "+687", "flag_nc.png"),
            new Country("NE", "Niger", "+227", "flag_ne.png"),
            new Country("NF", "Norfolk Island", "+672", "flag_nf.png"),
            new Country("NG", "Nigeria", "+234", "flag_ng.png"),
            new Country("NI", "Nicaragua", "+505", "flag_ni.png"),
            new Country("NL", "Netherlands", "+31", "flag_nl.png"),
            new Country("NO", "Norway", "+47", "flag_no.png"),
            new Country("NP", "Nepal", "+977", "flag_np.png"),
            new Country("NR", "Nauru", "+674", "flag_nr.png"),
            new Country("NU", "Niue", "+683", "flag_nu.png"),
            new Country("NZ", "New Zealand", "+64", "flag_nz.png"),
            new Country("OM", "Oman", "+968", "flag_om.png"),
            new Country("PA", "Panama", "+507", "flag_pa.png"),
            new Country("PE", "Peru", "+51", "flag_pe.png"),
            new Country("PF", "French Polynesia", "+689", "flag_pf.png"),
            new Country("PG", "Papua New Guinea", "+675", "flag_pg.png"),
            new Country("PH", "Philippines", "+63", "flag_ph.png"),
            new Country("PK", "Pakistan", "+92", "flag_pk.png"),
            new Country("PL", "Poland", "+48", "flag_pl.png"),
            new Country("PM", "Saint Pierre and Miquelon", "+508", "flag_pm.png"),
            new Country("PN", "Pitcairn", "+872", "flag_pn.png"),
            new Country("PR", "Puerto Rico", "+1", "flag_pr.png"),
            new Country("PS", "Palestinian Territory, Occupied", "+970", "flag_ps.png"),
            new Country("PT", "Portugal", "+351", "flag_pt.png"),
            new Country("PW", "Palau", "+680", "flag_pw.png"),
            new Country("PY", "Paraguay", "+595", "flag_py.png"),
            new Country("QA", "Qatar", "+974", "flag_qa.png"),
            new Country("RE", "Réunion", "+262", "flag_re.png"),
            new Country("RO", "Romania", "+40", "flag_ro.png"),
            new Country("RS", "Serbia", "+381", "flag_rs.png"),
            new Country("RU", "Russia", "+7", "flag_ru.png"),
            new Country("RW", "Rwanda", "+250", "flag_rw.png"),
            new Country("SA", "Saudi Arabia", "+966", "flag_sa.png"),
            new Country("SB", "Solomon Islands", "+677", "flag_sb.png"),
            new Country("SC", "Seychelles", "+248", "flag_sc.png"),
            new Country("SD", "Sudan", "+249", "flag_sd.png"),
            new Country("SE", "Sweden", "+46", "flag_se.png"),
            new Country("SG", "Singapore", "+65", "flag_sg.png"),
            new Country("SH", "Saint Helena", "+290", "flag_sh.png"), // , Ascension and Tristan Da Cunha
            new Country("SI", "Slovenia", "+386", "flag_si.png"),
            new Country("SJ", "Svalbard and Jan Mayen", "+47", "flag_sj.png"),
            new Country("SK", "Slovakia", "+421", "flag_sk.png"),
            new Country("SL", "Sierra Leone", "+232", "flag_sl.png"),
            new Country("SM", "San Marino", "+378", "flag_sm.png"),
            new Country("SN", "Senegal", "+221", "flag_sn.png"),
            new Country("SO", "Somalia", "+252", "flag_so.png"),
            new Country("SR", "Suriname", "+597", "flag_sr.png"),
            new Country("SS", "South Sudan", "+211", "flag_ss.png"),
            new Country("ST", "Sao Tome and Principe", "+239", "flag_st.png"),
            new Country("SV", "El Salvador", "+503", "flag_sv.png"),
            new Country("SX", "Sint Maarten", "+1", "flag_sx.png"),
            new Country("SY", "Syrian Arab Republic", "+963", "flag_sy.png"),
            new Country("SZ", "Swaziland", "+268", "flag_sz.png"),
            new Country("TC", "Turks and Caicos Islands", "+1", "flag_tc.png"),
            new Country("TD", "Chad", "+235", "flag_td.png"),
            new Country("TF", "French Southern Territories", "+262", "flag_tf.png"),
            new Country("TG", "Togo", "+228", "flag_tg.png"),
            new Country("TH", "Thailand", "+66", "flag_th.png"),
            new Country("TJ", "Tajikistan", "+992", "flag_tj.png"),
            new Country("TK", "Tokelau", "+690", "flag_tk.png"),
            new Country("TL", "East Timor", "+670", "flag_tl.png"),
            new Country("TM", "Turkmenistan", "+993", "flag_tm.png"),
            new Country("TN", "Tunisia", "+216", "flag_tn.png"),
            new Country("TO", "Tonga", "+676", "flag_to.png"),
            new Country("TR", "Turkey", "+90", "flag_tr.png"),
            new Country("TT", "Trinidad and Tobago", "+1", "flag_tt.png"),
            new Country("TV", "Tuvalu", "+688", "flag_tv.png"),
            new Country("TW", "Taiwan", "+886", "flag_tw.png"),
            new Country("TZ", "Tanzania", "+255", "flag_tz.png"), // , United Republic of
            new Country("UA", "Ukraine", "+380", "flag_ua.png"),
            new Country("UG", "Uganda", "+256", "flag_ug.png"),
            new Country("UM", "U.S. Minor Outlying Islands", "", "flag_um.png"),
            new Country("US", "United States", "+1", "flag_us.png"),
            new Country("UY", "Uruguay", "+598", "flag_uy.png"),
            new Country("UZ", "Uzbekistan", "+998", "flag_uz.png"),
            new Country("VA", "Holy See (Vatican City State)", "+379", "flag_va.png"),
            new Country("VC", "Saint Vincent and the Grenadines", "+1", "flag_vc.png"),
            new Country("VE", "Venezuela", "+58", "flag_ve.png"), // , Bolivarian Republic of
            new Country("VG", "Virgin Islands, British", "+1", "flag_vg.png"),
            new Country("VI", "Virgin Islands, U.S.", "+1", "flag_vi.png"),
            new Country("VN", "Viet Nam", "+84", "flag_vn.png"),
            new Country("VU", "Vanuatu", "+678", "flag_vu.png"),
            new Country("WF", "Wallis and Futuna", "+681", "flag_wf.png"),
            new Country("WS", "Samoa", "+685", "flag_ws.png"),
            new Country("XK", "Kosovo", "+383", "flag_xk.png"),
            new Country("YE", "Yemen", "+967", "flag_ye.png"),
            new Country("YT", "Mayotte", "+262", "flag_yt.png"),
            new Country("ZA", "South Africa", "+27", "flag_za.png"),
            new Country("ZM", "Zambia", "+260", "flag_zm.png"),
            new Country("ZW", "Zimbabwe", "+263", "flag_zw.png")
        };
    }
}
