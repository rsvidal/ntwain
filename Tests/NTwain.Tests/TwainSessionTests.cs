﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using NTwain.Data;
using NTwain.Internals;
using System;

namespace NTwain.Tests
{
    [TestClass]
    public class TwainSessionTests
    {
        [TestMethod]
        [ExpectedException(typeof(TwainStateException), "State check failed to throw.")]
        public void VerifyState_Throws_When_State_Is_Enforced()
        {
            ITwainSessionInternal session = new TwainSession(TWIdentity.Create(DataGroups.Image, new Version(1, 0), "test", "test", "test", "test"));
            session.EnforceState = true;
            session.ChangeState(4, false);

            session.VerifyState(6, 6, DataGroups.Image, DataArgumentType.ImageNativeXfer, Message.Get);
        }

        [TestMethod]
        public void VerifyState_No_Throws_When_State_Is_Not_Enforced()
        {
            ITwainSessionInternal session = new TwainSession(TWIdentity.Create(DataGroups.Image, new Version(1, 0), "test", "test", "test", "test"));
            session.EnforceState = false;
            session.ChangeState(4, false);

            session.VerifyState(6, 6, DataGroups.Image, DataArgumentType.ImageNativeXfer, Message.Get);
        }
    }
}
