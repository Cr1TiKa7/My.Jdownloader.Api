﻿using My.JDownloader.Api.Models.AccountV2.Types;

namespace My.JDownloader.Api.Models.AccountV2.Response
{
    public class ListBasicAuthResponse
    {
        public long Created { get; set; }
        public bool Enabled { get; set; }
        public string Hostmask { get; set; }
        public long Id { get; set; }
        public long LastValidated { get; set; }
        public string Password { get; set; }
        public HostType Type { get; set; }
        public string Username { get; set; }
    }
}
