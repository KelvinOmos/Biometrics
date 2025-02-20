﻿using System;
namespace Biometrics.Models
{
    public class MakeCredentialModel
    {
        public string UserId { get; set; } // The ID of the user for whom the credential is being created
        public string Challenge { get; set; } // A random challenge generated by the server
        public string RpId { get; set; } // The relying party identifier (usually the domain of the server)
        public string UserName { get; set; } // The username of the user
        public string DisplayName { get; set; } // The display name of the user
        public string Attestation { get; set; } // The attestation type (e.g., direct, indirect, none)
        public string AuthenticatorAttachment { get; set; } // The type of authenticator (e.g., platform, cross-platform)
        public string UserVerification { get; set; } // The user verification requirement (e.g., required, preferred, discouraged)
        public string ResidentKey { get; set; } // The resident key requirement (e.g., required, preferred, discouraged)
        public string PublicKeyCredentialRpEntity { get; set; } // The relying party entity
        public string PublicKeyCredentialUserEntity { get; set; } // The user entity
        public string PublicKeyCredentialParameters { get; set; } // The public key credential parameters
        public string PublicKeyCredentialDescriptor { get; set; } // The public key credential descriptor
        public string PublicKeyCredentialCreationOptions { get; set; } // The public key credential creation options
    }
}