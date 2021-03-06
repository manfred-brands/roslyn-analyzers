﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Immutable;
using Analyzer.Utilities;
using Analyzer.Utilities.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace Microsoft.NetCore.Analyzers.Usage
{
    [DiagnosticAnalyzer(LanguageNames.CSharp, LanguageNames.VisualBasic)]
    public sealed class ProvideCorrectArgumentToEnumHasFlag : DiagnosticAnalyzer
    {
        internal const string RuleId = "CA2248";

        private static readonly LocalizableString s_localizableTitle = new LocalizableResourceString(nameof(MicrosoftNetCoreAnalyzersResources.ProvideCorrectArgumentToEnumHasFlagTitle), MicrosoftNetCoreAnalyzersResources.ResourceManager, typeof(MicrosoftNetCoreAnalyzersResources));
        private static readonly LocalizableString s_localizableMessage = new LocalizableResourceString(nameof(MicrosoftNetCoreAnalyzersResources.ProvideCorrectArgumentToEnumHasFlagMessage), MicrosoftNetCoreAnalyzersResources.ResourceManager, typeof(MicrosoftNetCoreAnalyzersResources));
        private static readonly LocalizableString s_localizableDescription = new LocalizableResourceString(nameof(MicrosoftNetCoreAnalyzersResources.ProvideCorrectArgumentToEnumHasFlagDescription), MicrosoftNetCoreAnalyzersResources.ResourceManager, typeof(MicrosoftNetCoreAnalyzersResources));

        internal static DiagnosticDescriptor Rule = DiagnosticDescriptorHelper.Create(
            RuleId,
            s_localizableTitle,
            s_localizableMessage,
            DiagnosticCategory.Usage,
            RuleLevel.IdeSuggestion,
            description: s_localizableDescription,
            isPortedFxCopRule: false,
            isDataflowRule: false);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);

            context.RegisterOperationAction(context =>
            {
                var invocation = (IInvocationOperation)context.Operation;

                if (invocation.TargetMethod.ContainingType.SpecialType == SpecialType.System_Enum &&
                    invocation.Arguments.Length == 1 &&
                    invocation.Instance != null &&
                    invocation.TargetMethod.Name == "HasFlag" &&
                    invocation.Arguments[0].Value is IConversionOperation conversion &&
                    !invocation.Instance.Type.Equals(conversion.Operand.Type))
                {
                    context.ReportDiagnostic(invocation.CreateDiagnostic(Rule, GetArgumentTypeName(conversion), invocation.Instance.Type.Name));
                }
            }, OperationKind.Invocation);
        }

        private static string GetArgumentTypeName(IConversionOperation conversion)
        {
            if (conversion.Operand.Type != null)
            {
                return conversion.Operand.Type.Name;
            }

            return conversion.Language == LanguageNames.VisualBasic
                ? "<Nothing>"
                : "<null>";
        }
    }
}
